using System;
using System.Collections.Generic;

namespace LogAPI.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string Ip { get; set; }
        public string Request { get; set; }
    }
}
