using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LogAPI.Models
{
    public partial class Request
    {
        public int RequestId { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "DashboardId is required")]
        public int DashboardId { get; set; }
        public string ImageUrl { get; set; }
        public string Message { get; set; }

        [Required(ErrorMessage = "OrgId is required")]
        public int OrgId { get; set; }

        [Required(ErrorMessage = "PanelId is required")]
        public int PanelId { get; set; }

        [Required(ErrorMessage = "RuleId is required")]
        public int RuleId { get; set; }

        [Required(ErrorMessage = "RuleName is required")]
        public string RuleName { get; set; }

        [Required(ErrorMessage = "RuleUrl is required")]
        public string RuleUrl { get; set; }
        public string Title { get; set; }
    }
}
