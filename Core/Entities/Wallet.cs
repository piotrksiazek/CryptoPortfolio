using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Wallet
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string AppUserId { get; set; }
        [Required]
        public AppUser AppUser { get; set; }
        [Required]
        public int WalletTypeId { get; set; }
        [Required]
        public WalletType WalletType { get; set; }
    }
}
