using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public List<OrderItem> Items { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string Payment { get; set; }
        public string Status { get; set; }
        public string Email { get; set; }
    }

    public class OrderItem
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string DriveUrl { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedAt { get; set; }
    }
}