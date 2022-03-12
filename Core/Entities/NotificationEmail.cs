using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class NotificationEmail
    {
        public string Reciever { get; set; }
        public string Sender { get; set; }
        public string Title  { get; set; }
        public string Body { get; set; }
    }
}
