using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Server.Data;
using Server.Interfaces;
using Server.Models;

namespace Server.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly IMongoCollection<FileRecord> _collection;
        //private readonly MongoDbContext _context;

        public MenuRepository(MongoDbContext context)
        {
            _collection = context.Files;
        }

        // public async Task<List<FileRecord>> GetAllAsync()
        // {
        //     var collection = _context.Database.GetCollection<FileRecord>("Files");
        //     var allFiles = await collection.Find(_ => true).ToListAsync();
        //     return allFiles;
        // }

        // public async Task<List<FileRecord>> GetAllAsync()
        // {
        //     Console.WriteLine("[MenuRepository] GetAllAsync called");
        //     var result = await _collection.Find(_ => true).ToListAsync();
        //     Console.WriteLine($"[MenuRepository] Found {result.Count} files");
        //     return result;
        // }

        public async Task<List<FileRecord>> GetAllAsync()
        {
            //Console.WriteLine("[MenuRepository] GetAllAsync called");

            var bsonDocs = await _collection.Find(_ => true).ToListAsync();
            Console.WriteLine($"[MenuRepository] Raw Count: {bsonDocs.Count}");

            foreach (var doc in bsonDocs)
            {
                Console.WriteLine($"File: {doc.FileName}, Price: {doc.Price}");
            }

            return bsonDocs;
        }

       public async Task<FileRecord?> DeleteAsync(string id)
    {
        try
        {
            // ค้นหาและลบเอกสารจากคอลเล็กชัน
            var filter = Builders<FileRecord>.Filter.Eq(f => f.Id, id);
            var deletedFile = await _collection.FindOneAndDeleteAsync(filter);

            // ถ้าพบเอกสารที่ต้องการลบ จะคืนค่ากลับไป
            return deletedFile;
        }
        catch (Exception ex)
        {
            // หากเกิดข้อผิดพลาดใด ๆ สามารถจับและจัดการได้ที่นี่
            Console.WriteLine("Error deleting file: " + ex.Message);
            return null;
        }
    }

    }
}