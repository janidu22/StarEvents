using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class Report : BaseEntity
    {
        // Changed FK type to string if referencing ApplicationUser
        public string GeneratedBy { get; set; } = string.Empty;
        public string ReportType { get; set; } = string.Empty; // Sales, Users, Events, Revenue
        public DateTime GeneratedDate { get; set; }
        public string FilePath { get; set; } = string.Empty;
    }
}

