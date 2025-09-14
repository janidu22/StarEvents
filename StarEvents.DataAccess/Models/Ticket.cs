using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class Ticket : BaseEntity
    {
       
        public int EventID { get; set; }
        public string TicketType { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public int QuantitySold { get; set; }

        // Navigation
        public Event Event { get; set; } = null!;
        public ICollection<BookingDetails> BookingDetails { get; set; } = new List<BookingDetails>();
        public ICollection<ETicket> ETickets { get; set; } = new List<ETicket>();
    }
}
