using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public  string? Id { get; set; }
        public  string? UserName { get; set; }
        public  string? LastName { get; set; }

        public  string? Email { get; set; }

        public  string? PasswordHash { get; set; } // บันทึก hash password

        public string Role { get; set; } = "User"; // ใส่ role ถ้าต้องการ
    }
}
// using MongoDB.Bson;
// using MongoDB.Bson.Serialization.Attributes;

// namespace Server.Models
// {
//     public class User
//     {
//         [BsonId]
//         [BsonRepresentation(BsonType.ObjectId)]
//         public required string Id { get; set; }
//         public required string UserName { get; set; }
//         public required string LastName { get; set; }

//         public required string Email { get; set; }

//         public required string PasswordHash { get; set; } // บันทึก hash password

//         public string Role { get; set; } = "User"; // ใส่ role ถ้าต้องการ
//     }
// }

// // using System;
// // using System.Collections.Generic;
// // using System.Linq;
// // using System.Text.Json.Serialization;
// // using System.Threading.Tasks;
// // using MongoDB.Bson;
// // using MongoDB.Bson.Serialization.Attributes;
// // using AspNetCore.Identity.Mongo.Model;

// // namespace Server.Models
// // {
// //     public class AppUser : MongoIdentityUser
// //     {
// //         // [BsonId]// กำหนดว่า property นี้เป็น Primary Key ของ MongoDB
// //         // [BsonRepresentation(BsonType.ObjectId)]// แปลง ObjectId ให้เป็น string เพื่อใช้งานง่ายใน .NET
// //         // [JsonIgnore] // ✅ ซ่อนจาก Swagger และ model binding
// //         // public string? Id { get; set; } // required = ต้องใส่ค่า (ใช้ได้ใน .NET 7+)
// //          [BsonElement("customUserName")]
// //         public required string UserName { get; set; }
// //         public required string LastName { get; set; }
// //         [BsonElement("customEmail")]
// //         public required string Email { get; set; }
// //         public required string Password { get; set; }
// //     }

// //     public class MongoIdentityUser
// //     {
// //     }
// // }