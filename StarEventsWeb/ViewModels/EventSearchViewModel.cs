using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StarEventsWeb.ViewModels
{
    public class EventListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public decimal TicketPrice { get; set; }
        public string VenueName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public bool IsFree => TicketPrice <= 0;
    }

    public class EventSearchViewModel
    {
        // Filters
        public string? Query { get; set; }
        public string? Category { get; set; }
        public string? Location { get; set; }
        [Display(Name = "From")]
        public DateTime? StartFrom { get; set; }
        [Display(Name = "To")]
        public DateTime? EndTo { get; set; }

        // Options
        public IEnumerable<SelectListItem> Categories { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Locations { get; set; } = Array.Empty<SelectListItem>();

        // Results
        public List<EventListItemViewModel> Results { get; set; } = new();
    }
}
