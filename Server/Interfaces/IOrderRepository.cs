using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;

namespace Server.Interfaces
{
    public interface IOrderRepository
    {
         Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetByEmailAsync(string email);
        Task<bool> UpdateStatusAsync(string orderId, string newStatus);
        Task CreateAsync(Order order);
    }
}