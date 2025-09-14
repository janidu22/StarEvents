using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace StarEventsWeb.ViewModels
{
    public class EventFormViewModel
    {
        public int? Id { get; set; }

        [Display(Name = "Organizer")]
        public int? OrganizerID { get; set; }

        [Required]
        [Display(Name = "Venue")]
        public int VenueID { get; set; }

        [Required]
        [Display(Name = "Loyalty Ref")]
        public int LoyalityId { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;

        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Start")]
        public DateTime? StartDateTime { get; set; }

        [Required]
        [Display(Name = "End")]
        public DateTime? EndDateTime { get; set; }

        [Range(0, double.MaxValue)]
        [Display(Name = "Ticket Price")] 
        public decimal TicketPrice { get; set; }

        [Range(1, int.MaxValue)]
        [Display(Name = "Total Tickets")]
        public int TotalTickets { get; set; }

        [Display(Name = "Available Tickets")]
        public int? AvailableTickets { get; set; }

        [Display(Name = "Active")]
        public bool IsActive { get; set; }

        // Dropdowns
        public IEnumerable<SelectListItem> Venues { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem> Loyalities { get; set; } = Array.Empty<SelectListItem>();
        public IEnumerable<SelectListItem>? Organizers { get; set; }

        public bool IsAdmin { get; set; }
    }
}
