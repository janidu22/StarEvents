using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class BookingDetails : BaseEntity
    {
      
        public int BookingID { get; set; }
        public int TicketID { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }

        // Navigation
        public Booking Booking { get; set; } = null!;
        public Ticket Ticket { get; set; } = null!;
    }
}
