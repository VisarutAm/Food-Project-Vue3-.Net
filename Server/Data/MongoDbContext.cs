using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Server.Models;


namespace Server.Data
{
    public class MongoDbContext
{
    private readonly IMongoDatabase _db;

    public MongoDbContext(IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
        var client = new MongoClient(connectionString);
        _db = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
    }

    public IMongoCollection<AppUser> Users =>
        _db.GetCollection<AppUser>("Users");

    //public IMongoDatabase Database=> _db;  
    public IMongoCollection<FileRecord> Files => _db.GetCollection<FileRecord>("Files");  
}

    
}

// MongoClient	ใช้เชื่อมต่อ MongoDB Atlas
// configuration["MongoDB:ConnectionString"]	ดึงค่าจาก appsettings.json หรือ .env ผ่าน IConfiguration
// _db.GetCollection<AppUser1>("Users")	เข้าถึง collection "Users" ซึ่งผูกกับ model AppUser1
// AppUser1	ต้องมีอยู่ใน namespace Server.Models