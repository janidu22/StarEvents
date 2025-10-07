using System;
using System.ComponentModel.DataAnnotations;

namespace StarEventsWeb.ViewModels
{
    public class BuyTicketViewModel
    {
        public int EventId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string VenueName { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public decimal TicketPrice { get; set; }
        public int AvailableTickets { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")] 
        public int Quantity { get; set; } = 1;
        public decimal Total => TicketPrice * Quantity;
    }
}
