using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class FileRecord
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string FileName { get; set; }
        public string DriveUrl { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        [BsonRepresentation(BsonType.String)]
        public decimal Price { get; set; }
    }
}