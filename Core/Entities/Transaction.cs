using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Transaction : IOwnedByUser
    {
        [Required]
        public decimal UnitPrice { get; set; }
        [Required]
        public decimal Amount { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        public int CryptocurrencyId { get; set; }
        public Cryptocurrency Cryptocurrency { get; set; }
    }
}
