using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapr;
using Dapr.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly DaprClient _daprClient;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(DaprClient daprClient, ILogger<InventoryController> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        [HttpPost("check-inventory")]
        [Topic("pubsub", "OrderSubmitted")]
        public async Task<IActionResult> CheckInventoryForOrder([FromBody] OrderSubmitted order)
        {
            _logger.LogInformation($"Order {order.Id} with ${order.Amount} is being checked for available inventory...");
            await _daprClient.PublishEventAsync("pubsub", "OrderConfirmed", order);
            return Ok();
        }
    }

    public class OrderSubmitted
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
    }
}
