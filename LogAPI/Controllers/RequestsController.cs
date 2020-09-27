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
using Newtonsoft.Json;
using System.Web;

namespace LogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestsController : ControllerBase
    {
        private readonly MQContext _context;
        private readonly IHttpContextAccessor _contextAccessor;
        public RequestsController(MQContext context, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        [HttpPost]
        public async Task<IActionResult> PostRequest([FromBody] Request request)
        {
            _context.Request.Add(request);

            var log = new Log
            {
                Request = JsonConvert.SerializeObject(request),
                Ip = "192.168.0.1",
                DashboardId = request.DashboardId,
                Message = request.Message,
                OrgId = request.OrgId,
                PanelId = request.PanelId,
                RuleId = request.RuleId
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

                byte[] body = Encoding.Default.GetBytes(JsonConvert.SerializeObject(request));

                channel.BasicPublish(exchange: "",
                                     routingKey: "RequestQueue",
                                     basicProperties: null,
                                     body: body);

                return Ok(request);
            }
        }
    }
}
