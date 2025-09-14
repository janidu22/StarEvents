using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class EventOrganizer : BaseEntity
    {
        // Changed FK type from int to string to match IdentityUser key
        public string UserId { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string ContactPerson { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty;
        public string BankAccountDetails { get; set; } = string.Empty;

        // Navigation
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
