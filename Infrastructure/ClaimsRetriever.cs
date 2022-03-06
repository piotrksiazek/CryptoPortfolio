using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ClaimsRetriever : IClaimsRetriever
    {
        public string GetUserId(HttpContext httpContext)
        {
            if (httpContext.User == null)
            {
                return "";
            }
            return httpContext.User.Claims.Single(x => x.Type == "id").Value;
        }
    }
}
