using System.Collections.Generic;

namespace StarEventsWeb.ViewModels
{
    public class TicketCardVm
    {
        public int ETicketId { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Svg { get; set; } = string.Empty;
        public bool IsUsed { get; set; }
    }

    public class BookingTicketsViewModel
    {
        public int BookingId { get; set; }
        public string EventTitle { get; set; } = string.Empty;
        public string EventDateText { get; set; } = string.Empty;
        public int Count { get; set; }
        public List<TicketCardVm> Tickets { get; set; } = new();
    }
}
