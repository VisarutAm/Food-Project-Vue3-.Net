using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Server.DTOs.Payment;
using Stripe.Checkout;
using DotNetEnv;

namespace Server.Controllers
{
[ApiController]
    [Route("api/order")]
    public class PaymentController : ControllerBase
    {
        public PaymentController()
    {
        DotNetEnv.Env.Load(); // โหลด .env
    }
    
         [HttpPost("create-checkout-session")]
    public IActionResult CreateCheckoutSession([FromBody] OrderDto order)
    {
         var secretKey = Environment.GetEnvironmentVariable("STRIPE_SECRET_KEY");
          Console.WriteLine($"STRIPE_SECRET_KEY = {secretKey}");
        Stripe.StripeConfiguration.ApiKey = secretKey; 

        var lineItems = order.Items.Select(item => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                Currency = "thb",
                UnitAmount = (long)(item.Price * 100), // หน่วยเป็นสตางค์
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
            SuccessUrl = "http://localhost:5173",
            // SuccessUrl = "http://localhost:5173/success?session_id={CHECKOUT_SESSION_ID}", // 🔁 จะกลับมาที่นี่
            CancelUrl = "http://localhost:5173/admin"
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Ok(new { url = session.Url });
    }
    }
}