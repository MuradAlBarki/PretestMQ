using System;
using System.Collections.Generic;

namespace LogAPI.Models
{
    public partial class Log
    {
        public int LogId { get; set; }
        public string Ip { get; set; }
        public string Request { get; set; }
        public int DashboardId { get; set; }
        public string Message { get; set; }
        public int OrgId { get; set; }
        public int PanelId { get; set; }
        public int RuleId { get; set; }
    }
}
