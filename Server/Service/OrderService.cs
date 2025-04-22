using MongoDB.Driver;
using Server.Models;
using Microsoft.Extensions.Configuration;
using DotNetEnv;
using Server.Interfaces;


namespace Server.Service
{
    public class OrderService
    {
         private readonly IOrderRepository _orderRepository;


        private readonly IMongoCollection<Order> _orders;

        public OrderService(IConfiguration configuration,IOrderRepository orderRepository)
        {
             _orderRepository = orderRepository;
             DotNetEnv.Env.Load(); 


            var connectionString = Environment.GetEnvironmentVariable("MONGODB_CONNECTION");
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("MyDb");
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task CreateOrderAsync(Order order)
        {
            await _orders.InsertOneAsync(order);
        }

         public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<List<Order>> GetOrdersByEmailAsync(string email)
        {
            return await _orderRepository.GetByEmailAsync(email);
        }

        public async Task<bool> UpdateOrderStatusAsync(string id, string status)
        {
            return await _orderRepository.UpdateStatusAsync(id, status);
        }
    }
}

// using Server.Models;
// using Server.Interfaces;
// using System.Collections.Generic;
// using System.Threading.Tasks;

// namespace Server.Service
// {
//     public class OrderService
//     {
//         private readonly IOrderRepository _orderRepository;

//         public OrderService(IOrderRepository orderRepository)
//         {
//             _orderRepository = orderRepository;
//         }

//         public async Task CreateOrderAsync(Order order)
//         {
//             await _orderRepository.CreateAsync(order);
//         }

//         public async Task<List<Order>> GetAllOrdersAsync()
//         {
//             return await _orderRepository.GetAllAsync();
//         }

//         public async Task<List<Order>> GetOrdersByEmailAsync(string email)
//         {
//             return await _orderRepository.GetByEmailAsync(email);
//         }

//         public async Task<bool> UpdateOrderStatusAsync(string id, string status)
//         {
//             return await _orderRepository.UpdateStatusAsync(id, status);
//         }
//     }
// }

