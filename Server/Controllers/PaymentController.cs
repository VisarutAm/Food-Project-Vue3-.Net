using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Payment;
using Stripe.Checkout;
using DotNetEnv;
using Server.Service;
using Server.Models;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class PaymentController : ControllerBase
    {
        private readonly OrderService _orderService;
        public PaymentController(OrderService orderService)
        {
            _orderService = orderService;
            DotNetEnv.Env.Load(); // โหลด .env
        }

        // [HttpPost("payment")]
        // public async Task<IActionResult> CreateCheckoutSession([FromBody] OrderDto order)
        // {
        //     var secretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
        //     Console.WriteLine($"STRIPE_SECRET_KEY = {secretKey}");

        //     // 🔍 Debug ข้อมูล order
        //     Console.WriteLine("===== ORDER RECEIVED =====");
        //     Console.WriteLine($"Amount: {order.Amount}");
        //     Console.WriteLine($"Status: {order.Status}");
        //     Console.WriteLine($"Payment: {order.Payment}");
        //      Console.WriteLine($"Email: {order.Email}");
        //     Console.WriteLine($"Date: {order.Date}");
        //     Console.WriteLine("Items:");
        //     foreach (var item in order.Items)
        //     {
        //         Console.WriteLine($" - {item.Name} | Price: {item.Price} | Qty: {item.Quantity}");
        //     }
        //     Console.WriteLine("==========================");
        //     Stripe.StripeConfiguration.ApiKey = secretKey;

        //     var mongoOrder = new Order
        //     {
        //         Email=order.Email,
        //         Amount = order.Amount,
        //         Date = order.Date,
        //         Payment = "pending", // หรือยังไม่จ่ายจริง
        //         Status = order.Status,
        //         Items = order.Items.Select(i => new OrderItem
        //         {
        //             Id = i.Id,
        //             Name = i.Name,
        //             Quantity = i.Quantity,
        //             Price = i.Price,
        //             Category = i.Category,
        //             Description = i.Description,
        //             DriveUrl = i.DriveUrl,
        //             FileName = i.FileName,
        //             UploadedAt = i.UploadedAt
        //         }).ToList()
        //     };

        //     await _orderService.CreateOrderAsync(mongoOrder);
        //     Console.WriteLine("✅ Order saved to MongoDB");


        //     var lineItems = order.Items.Select(item => new SessionLineItemOptions
        //     {
        //         PriceData = new SessionLineItemPriceDataOptions
        //         {
        //             Currency = "thb",
        //             UnitAmount = (long)(item.Price * 100), // หน่วยเป็นสตางค์
        //             ProductData = new SessionLineItemPriceDataProductDataOptions
        //             {
        //                 Name = item.Name
        //             }
        //         },
        //         Quantity = item.Quantity
        //     }).ToList();

        //     var options = new SessionCreateOptions
        //     {
        //         LineItems = lineItems,
        //         Mode = "payment",
        //         SuccessUrl = "http://localhost:5173/success",
        //         // SuccessUrl = "http://localhost:5173/success?session_id={CHECKOUT_SESSION_ID}", // 🔁 จะกลับมาที่นี่
        //         CancelUrl = "http://localhost:5173/admin"
        //     };

        //     var service = new SessionService();
        //     Session session = service.Create(options);

        //     Console.WriteLine("Session URL: " + session.Url);
        //     return Ok(new { url = session.Url });
        // }
        [HttpPost("payment")]
        public IActionResult CreateCheckoutSession([FromBody] OrderDto order)
        {
            var secretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
            Stripe.StripeConfiguration.ApiKey = secretKey;

            var lineItems = order.Items.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "thb",
                    UnitAmount = (long)(item.Price * 100),
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.Name
                    }
                },
                Quantity = item.Quantity
            }).ToList();

            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                Mode = "payment",
                SuccessUrl = "http://localhost:5173/success",
                CancelUrl = "http://localhost:5173/cancel"
            };

            var service = new SessionService();
            Session session = service.Create(options);

            return Ok(new { url = session.Url });
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder([FromBody] OrderDto order)
        {
            // 🔍 Debug ข้อมูล order
            // Console.WriteLine("===== ORDER RECEIVED =====");
            // Console.WriteLine($"Amount: {order.Amount}");
            // Console.WriteLine($"Status: {order.Status}");
            // Console.WriteLine($"Payment: {order.Payment}");
            // Console.WriteLine($"Email: {order.Email}");
            // Console.WriteLine($"Date: {order.Date}");
            // Console.WriteLine("Items:");
            // foreach (var item in order.Items)
            // {
            //     Console.WriteLine($" - {item.Name} | Price: {item.Price} | Qty: {item.Quantity}");
            // }
            // Console.WriteLine("==========================");

            var mongoOrder = new Order
            {
                Email = order.Email,
                Amount = order.Amount,
                Date = order.Date,
                Payment = "Paid",
                Status = order.Status,
                Items = order.Items.Select(i => new OrderItem
                {
                    Id = i.Id,
                    Name = i.Name,
                    Quantity = i.Quantity,
                    Price = i.Price,
                    Category = i.Category,
                    Description = i.Description,
                    DriveUrl = i.DriveUrl,
                    FileName = i.FileName,
                    UploadedAt = i.UploadedAt
                }).ToList()
            };

            await _orderService.CreateOrderAsync(mongoOrder);
            return Ok(new { message = "Order saved!" });
        }


        [HttpGet("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetOrdersByEmail(string email)
        {
            var orders = await _orderService.GetOrdersByEmailAsync(email);
            return Ok(orders);
        }

        // [HttpPut("status/{id}")]
        // public async Task<IActionResult> UpdateOrderStatus(string id, [FromBody] string newStatus)
        // {
        //     var updated = await _orderService.UpdateOrderStatusAsync(id, newStatus);
        //     if (!updated)
        //         return NotFound(new { message = "Order not found or not updated" });

        //     return Ok(new { message = "Status updated" });
        // }

        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateOrderStatus(string id, [FromBody] UpdateStatusDto request)
        {
            var updated = await _orderService.UpdateOrderStatusAsync(id, request.NewStatus);
            if (!updated)
                return NotFound(new { message = "Order not found or not updated" });

            return Ok(new { message = "Status updated" });
        }


    }
}