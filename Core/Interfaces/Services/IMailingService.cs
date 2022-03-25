using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IMailingService
    {
        void SendEmailAsync(NotificationEmail email, Notification notification, IUnitOfWork unitOfWork,
            Action handleSuccesfulEmailMethod=null, Action handleFailedEmailMethod=null);
        void HandleSuccessfulEmail(Notification notification, IUnitOfWork uow);
        void HandleFailedEmail();
    }
}
