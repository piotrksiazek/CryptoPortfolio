using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Wallet : IOwnedByUser
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        [Required]
        public int WalletTypeId { get; set; }
        [Required]
        public WalletType WalletType { get; set; }
        [Required]
        public int CryptocurrencyId { get; set; }
        [Required]
        public Cryptocurrency Cryptocurrency { get; set; }
    }
}
