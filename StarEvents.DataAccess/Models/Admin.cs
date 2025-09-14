using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class Admin : BaseEntity
    {
        
        public string UserId { get; set; } = string.Empty;
        public string AccessLevel { get; set; } = string.Empty;
        public ApplicationUser User { get; set; } = null!;
    }
}
