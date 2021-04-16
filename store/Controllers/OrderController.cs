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
        private readonly DaprClient _daprClient;
        private readonly ILogger<OrderController> _logger;
        public const string StoreName = "orders-state";
        public OrderController(DaprClient daprClient, ILogger<OrderController> logger)
        {
            this._daprClient = daprClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<Order>> Get()
        {
            var order = await _daprClient.GetStateAsync<Order>(StoreName, "order");
            if (order == null) return NoContent();
            return order;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNewOrder(Order newOrder)
        {
            await _daprClient.SaveStateAsync(StoreName, "order", newOrder);
            return CreatedAtAction(nameof(Get), newOrder);
        }
    }
}
