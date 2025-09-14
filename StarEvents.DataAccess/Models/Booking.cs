using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class Booking : BaseEntity
    {
        public int CustomerID { get; set; }
        public int EventID { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentStatus { get; set; } = string.Empty; // Pending, Completed, Failed, Refunded

        // Navigation
        public Customer Customer { get; set; } = null!;
        public Event Event { get; set; } = null!;
        public ICollection<BookingDetails> BookingDetails { get; set; } = new List<BookingDetails>();
        public Payment? Payment { get; set; }
        public ICollection<ETicket> ETickets { get; set; } = new List<ETicket>(); // Added for mapping
    }
}
