using Core.Entities;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IPriceChangeNotificationService
    {
        NotificationType GetNotificationType(Notification notification, double price);
        void Notify(Notification notification, double price, IUnitOfWork uow, NotificationType notificationType);
        Task NotifyAll(IUnitOfWork uow);
    }
}
