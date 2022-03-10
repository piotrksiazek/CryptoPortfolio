using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Notification
    {
        [Required]
        public int Id { get; set; }
        public decimal PricePoint { get; set; }
        public bool GreaterThanOrEqual { get; set; }
        [Required]
        public bool IsRecurring { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [Required]
        public AppUser AppUser { get; set; }
        [Required]
        public int CryptocurrencyId { get; set; }
        [Required]
        public Cryptocurrency Cryptocurrency { get; set; }
    }
}
