﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IExternalApiClientService
    {
        public HttpClient ApiClient { get; set; }
    }
}