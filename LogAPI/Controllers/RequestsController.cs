using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogAPI.Models;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace LogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly MQContext _context;
        public RequestsController(MQContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] Request request)
        {
            _context.Request.Add(request);

            var log = new Log
            {
                Request = request.Status.ToString(),
                Ip = "192.168.0.1"
            };

            _context.Log.Add(log);

            await _context.SaveChangesAsync();


            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "RequestQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "the message has been sent and that's awesome";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "RequestQueue",
                                     basicProperties: null,
                                     body: body);

                return Ok(request);
            }
        }
    }
}
