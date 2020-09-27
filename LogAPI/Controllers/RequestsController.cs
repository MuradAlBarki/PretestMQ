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
using System.Net;
using System.Net.Sockets;

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
            await _context.SaveChangesAsync();

            await Logger(request);

            


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

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public async Task Logger(Request request)
        {
            var log = new Log
            {
                Request = JsonConvert.SerializeObject(request),
                Ip = GetLocalIPAddress(),
                DashboardId = request.DashboardId,
                Message = request.Message,
                OrgId = request.OrgId,
                PanelId = request.PanelId,
                RuleId = request.RuleId
            };

            _context.Log.Add(log);

            await _context.SaveChangesAsync();
        }
    }
}
