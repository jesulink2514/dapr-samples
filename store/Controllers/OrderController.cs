using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using store.Models;

namespace store.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly DaprClient daprClient;
        private readonly ILogger<OrderController> _logger;
        public const string StoreName = "";
        public OrderController(DaprClient daprClient, ILogger<OrderController> logger)
        {
            this.daprClient = daprClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Order> Get()
        {
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(Order newOrder)
        {

            return CreatedAtAction(nameof(Get), newOrder);
        }
    }
}
