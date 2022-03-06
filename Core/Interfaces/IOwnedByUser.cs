using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public class IOwnedByUser
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
    }
}
