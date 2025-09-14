using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class ETicket : BaseEntity
    {

        public int BookingID { get; set; }
        public int TicketID { get; set; }
        public string QRCode { get; set; } = string.Empty;
        public DateTime IssuedDate { get; set; }
        public bool IsUsed { get; set; }

        // Navigation
        public Booking Booking { get; set; } = null!;
        public Ticket Ticket { get; set; } = null!;
    }
}
