using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs
{
    public class MenuDto
    {
        public string Id { get; set; }

        public string FileName { get; set; }
        public string DriveUrl { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}