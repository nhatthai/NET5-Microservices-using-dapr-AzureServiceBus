using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dapr;

namespace Consumer.Controllers
{
    [ApiController]
    public class ProcessorController : ControllerBase
    {
        private readonly ILogger<ProcessorController> _logger;
        private const string DAPR_PUBSUB_NAME = "orderpubsub";
        private const string TOPIC_NAME = "orders";

        public ProcessorController(ILogger<ProcessorController> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        //Subscribe to a topic
        [Topic(DAPR_PUBSUB_NAME, TOPIC_NAME)]
        [HttpPost("processor")]
        public void Processor([FromBody] Messages.Commands.Order order)
        {
            Console.WriteLine("Subscriber received : " + order.OrderId);
            _logger.LogInformation("Received Order: {OrderId}, {OrderNumber} for Processor", order.OrderId, order.OrderNumber);
        }
    }
}