// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Google.Apis.Auth.OAuth2;
// using Google.Apis.Drive.v3;
// using Google.Apis.Services;
// using Google.Apis.Upload;
// using Google.Apis.Util.Store;
// using Microsoft.AspNetCore.Http;

// namespace Server.Service
// {
//     public class GoogleDriveService
//     {
//         private readonly DriveService _driveService;

//         public GoogleDriveService()
//         {
//             var credential = GoogleCredential.FromFile("client_secret.json")
//                 .CreateScoped(DriveService.Scope.Drive);

//                 Console.WriteLine("Credential:",credential);

//             if (credential == null)
//             {
//                 throw new Exception("Failed to load Google credentials.");
//             }

//             _driveService = new DriveService(new BaseClientService.Initializer()
//             {
//                 HttpClientInitializer = credential,
//                 ApplicationName = "DotNetDriveUploader"
//             });
//         }

//         public async Task<string> UploadFileAsync(IFormFile file)
//         {
//             var fileMetadata = new Google.Apis.Drive.v3.Data.File()
//             {
//                 Name = file.FileName,
//                 Parents = new List<string> { "1fRoK8UUzZu9mDX8YZ1yLlG5iO6yqDjqy?usp=drive_link" } // แก้เป็น Folder ID ใน Google Drive
//             };

//             using var stream = file.OpenReadStream();
//             var request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
//             request.Fields = "id";
//             await request.UploadAsync();

//             var uploadedFile = request.ResponseBody;

//             // ตั้งค่าให้เป็น public
//             var permission = new Google.Apis.Drive.v3.Data.Permission()
//             {
//                 Role = "reader",
//                 Type = "anyone"
//             };
//             await _driveService.Permissions.Create(permission, uploadedFile.Id).ExecuteAsync();

//             return $"https://drive.google.com/uc?id={uploadedFile.Id}";
//         }
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.AspNetCore.Http;

namespace Server.Service
{
    public class GoogleDriveService
    {
        private readonly DriveService _driveService;

        public GoogleDriveService()
        {
            // var clientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET");
            // Console.WriteLine($"Secret: {clientSecret}");
            var credential = GoogleCredential.FromFile("client_secret.json")
                .CreateScoped(DriveService.Scope.Drive);

           // Console.WriteLine("Credential:", credential);

            if (credential == null)
            {
                throw new Exception("Failed to load Google credentials.");
            }

            _driveService = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "DotNetDriveUploader"
            });
        }

        public async Task<string> UploadFileAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("No file uploaded.");
            }

            var fileMetadata = new Google.Apis.Drive.v3.Data.File()
            {
                Name = file.FileName,
                Parents = new List<string> { "1fRoK8UUzZu9mDX8YZ1yLlG5iO6yqDjqy" } // ใช้ Folder ID ที่ถูกต้อง
            };

            using var stream = file.OpenReadStream();
            var request = _driveService.Files.Create(fileMetadata, stream, file.ContentType);
            request.Fields = "id";

            try
            {
                var uploadResponse = await request.UploadAsync();
                if (uploadResponse.Status != UploadStatus.Completed)
                {
                    throw new Exception($"File upload failed with status: {uploadResponse.Status}");
                }

                var uploadedFile = request.ResponseBody;
                if (uploadedFile == null)
                {
                    throw new Exception("Failed to retrieve uploaded file metadata.");
                }

                // ตั้งค่าให้เป็น public
                var permission = new Google.Apis.Drive.v3.Data.Permission()
                {
                    Role = "reader",
                    Type = "anyone"
                };

                await _driveService.Permissions.Create(permission, uploadedFile.Id).ExecuteAsync();

                return $"https://drive.google.com/uc?id={uploadedFile.Id}";
            }
            catch (Exception ex)
            {
                // Log error (or handle it)
                Console.WriteLine($"Error during file upload: {ex.Message}");
                throw new Exception("File upload failed.", ex);
            }
        }

    }
}