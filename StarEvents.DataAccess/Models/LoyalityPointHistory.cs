using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarEvents.DataAccess.Models
{
    public  class LoyalityPointHistory : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty; // Human friendly label

        public int Points { get; set; }

        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;
    }
}
