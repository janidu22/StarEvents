using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class Customer : BaseEntity
    {
        // Changed FK type from int to string to match IdentityUser key
        public string UserId { get; set; } = string.Empty;
        public int LoyaltyPoints { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }

        // Navigation
        public ApplicationUser User { get; set; } = null!;
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public ICollection<LoyalityPointHistory> LoyaltyPointsHistory { get; set; } = new List<LoyalityPointHistory>();
    }
}
