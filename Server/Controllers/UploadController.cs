// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Server.Models;
// using MongoDB.Driver;
// using Server.Service;

// namespace Server.Controllers
// {
//      [ApiController]
//     [Route("api/food")]
//     public class UploadController : ControllerBase
//     {
//         private readonly GoogleDriveService _driveService;
//         private readonly IMongoCollection<FileRecord> _collection;

//         public UploadController(GoogleDriveService driveService, IConfiguration config)
//         {
//             _driveService = driveService;

//              var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
//              Console.WriteLine($"MongoDB ConnectionString: {connectionString}");
//             var mongoClient = new MongoClient(connectionString);
//             var database = mongoClient.GetDatabase("FileUploader");
//             _collection = database.GetCollection<FileRecord>("Files");
//         }

//         [HttpPost]
//         [Route("add")]
//         public async Task<IActionResult> UploadImage(IFormFile file)
//         {
//             if (file == null || file.Length == 0)
//                 return BadRequest("No file uploaded.");

//             var driveUrl = await _driveService.UploadFileAsync(file);

//             var fileRecord = new FileRecord
//             {
//                 FileName = file.FileName,
//                 DriveUrl = driveUrl
//             };

//             await _collection.InsertOneAsync(fileRecord);

//             return Ok(fileRecord);
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using MongoDB.Driver;
using Server.Service;
using Server.Interfaces;
using Server.DTOs;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/food")]
    public class UploadController : ControllerBase
    {
        private readonly GoogleDriveService _driveService;
        private readonly IMongoCollection<FileRecord> _collection;
        private readonly IMenuRepository _menuRepo;

        public UploadController(GoogleDriveService driveService, IConfiguration config, IMenuRepository menuRepo)
        {
            _driveService = driveService;
            _menuRepo = menuRepo;

            var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
            Console.WriteLine($"MongoDB ConnectionString: {connectionString}");
            var mongoClient = new MongoClient(connectionString);
            var database = mongoClient.GetDatabase("MyDb");
            _collection = database.GetCollection<FileRecord>("Files");
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> UploadImage(
            IFormFile file,
            [FromForm] string name,
            [FromForm] string description,
            [FromForm] string price,
            [FromForm] string category)
        {


            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            try
            {
                var driveUrl = await _driveService.UploadFileAsync(file);

                var fileRecord = new FileRecord
                {
                    FileName = file.FileName,
                    DriveUrl = driveUrl,
                    Name = name,
                    Description = description,
                    Category = category,
                    Price = decimal.TryParse(price, out var parsedPrice) ? parsedPrice : 0

                };
                Console.WriteLine($"name: {name}, description: {description}, price: {price}, category: {category}");

                await _collection.InsertOneAsync(fileRecord);

                return Ok(new
                {
                    success = true,
                    message = "File uploaded successfully",
                    data = fileRecord
                });
            }
            catch (Exception ex)
            {
                // Log error and send user-friendly message
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, "Internal server error while uploading file.");
            }
        }

        [HttpGet]
        [Route("allmenu")]
        public async Task<IActionResult> GetAll()
        {
            Console.WriteLine("[UploadController] GET /api/food called");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var allMenu = await _menuRepo.GetAllAsync();
            return Ok(allMenu);
        }

        [HttpDelete]
        [Route("delete/{id}")]

        public async Task<IActionResult> DeleteFile([FromRoute] string id)
        {
            Console.WriteLine("[UploadController] Delete /api/food/delete/{id} called");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var deletedFile = await _menuRepo.DeleteAsync(id);

            if (deletedFile == null)
            {
                return NotFound(new { message = "File not found" });
            }

            return Ok(new { message = "File deleted successfully", file = deletedFile });

        }


    }
}