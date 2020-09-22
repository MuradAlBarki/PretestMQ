using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LogAPI.Models;
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
            return Ok(request);
        }
    }
}
