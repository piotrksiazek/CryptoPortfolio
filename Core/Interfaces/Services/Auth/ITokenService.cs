using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services.Auth
{
    public interface ITokenService
    {
        string GenerateToken(AppUser user);
    }
}
