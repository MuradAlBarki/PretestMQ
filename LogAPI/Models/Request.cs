using System;
using System.Collections.Generic;

namespace LogAPI.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }
        public string State { get; set; }
        public int DashboardId { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }
        public int OrgId { get; set; }
        public int PanelId { get; set; }
        public int RuleId { get; set; }
        public string RuleName { get; set; }
        public string RuleUrl { get; set; }
        public string Title { get; set; }
    }
}
