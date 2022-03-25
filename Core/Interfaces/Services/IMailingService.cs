using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IMailingService
    {
        void SendEmailAsync(NotificationEmail email);
        void HandleSuccessfulEmail();
        void HandleFailedEmail();
    }
}
