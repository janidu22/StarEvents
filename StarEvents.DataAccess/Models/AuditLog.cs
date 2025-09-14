using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class AuditLog : BaseEntity
    {
       
        public string UserId { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string Entity { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; }
        public string IPAddress { get; set; } = string.Empty;
    }
}
