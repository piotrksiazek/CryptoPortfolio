using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IExternalApiClientService
    {
        /// <summary>
        /// Calls resource and parses it to json
        /// </summary>
        /// <returns>json</returns>
        public Task<string?> Get(string url);
    }
}