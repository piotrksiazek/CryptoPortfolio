using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class AppUser : IdentityUser
    {
        public ICollection<Wallet> Wallets { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Balance> Balances { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
