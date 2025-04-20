using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class AppUser1
    {
        [BsonId]// กำหนดว่า property นี้เป็น Primary Key ของ MongoDB
        [BsonRepresentation(BsonType.ObjectId)]// แปลง ObjectId ให้เป็น string เพื่อใช้งานง่ายใน .NET
        [JsonIgnore] // ✅ ซ่อนจาก Swagger และ model binding
        public string? Id { get; set; } // required = ต้องใส่ค่า (ใช้ได้ใน .NET 7+)    

        public required string UserName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? PasswordHash { get; set; }

        public string Role { get; set; } = "User";
    }
}