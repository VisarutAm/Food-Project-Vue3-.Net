using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using Server.Models;
using Server.Interfaces;

namespace Server.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _context;

        public OrderRepository(IMongoCollection<Order> context)
        {
             _context = context;
        }
        
        public async Task<List<Order>> GetAllAsync() 
        {

             return await _context.Find(_ => true).ToListAsync();
        }

         public async Task<List<Order>> GetByEmailAsync(string email)
        {
            return await _context
        .Find(o => o.Email == email)
        .SortByDescending(o => o.Date)
        .ToListAsync();
        }

        public async Task<bool> UpdateStatusAsync(string orderId, string newStatus)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.Id, orderId);
            var update = Builders<Order>.Update.Set(o => o.Status, newStatus);

            var result = await _context.UpdateOneAsync(filter, update);
            return result.ModifiedCount > 0;
        }

        public async Task CreateAsync(Order order)
        {
            await _context.InsertOneAsync(order);
        }
        
    }
}