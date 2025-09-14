using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class Event : BaseEntity
    {
        public int OrganizerID { get; set; }
        public int VenueID { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal TicketPrice { get; set; }
        public int TotalTickets { get; set; }
        public int AvailableTickets { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; } = false;
        public int LoyalityId { get; set; }
      
        public EventOrganizer? Organizer { get; set; }
        public LoyalityPointHistory? Loyality { get; set; }
        public Venue? Venue { get; set; }
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
