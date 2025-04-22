using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.DTOs.Payment
{
    public class OrderDto
{
    public List<ItemDto> Items { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }  
      public string Payment { get; set; }
    public string Status { get; set; }
    public string Email { get; set; }
   
}

public class ItemDto
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