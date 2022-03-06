using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class Predicates
    {
        public static Func<IOwnedByUser, string, bool> IsOwnedByUser = (thisUser, otherUser) => thisUser.AppUserId == otherUser;
    }
}
