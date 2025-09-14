using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public class Payment : BaseEntity
    {
       
        public int BookingID { get; set; }
        public string PaymentMethod { get; set; } = string.Empty; // Stripe/PayPal
        public DateTime PaymentDate { get; set; }
        public decimal AmountPaid { get; set; }
        //public string TransactionID { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;

        // Navigation
        public Booking Booking { get; set; } = null!;
    }
}
