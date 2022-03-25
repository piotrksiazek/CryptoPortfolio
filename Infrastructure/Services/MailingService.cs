using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MailingService : IMailingService
    {
        public Action HandleSuccessfulEmailDelegate { get; set; }
        public Action HandleFailedEmailDelegate { get; set; }

        private readonly string? _username;
        private string? _password;
        private readonly SmtpClient _client;

        /// <summary>
        /// Ctor parameters for testing purpouse. In application should remain null.
        /// </summary>
        /// <param name="handleSuccesfulEmailMethod"></param>
        /// <param name="handleFailedEmailMethod"></param>
        /// <exception cref="Exception"></exception>
        public MailingService()
        {
            _password = Environment.GetEnvironmentVariable("CryptoPortfolioEmailPassword");
            _username = Environment.GetEnvironmentVariable("CryptoPortfolioEmailUsername");
            if(_password == null)
            {
                throw new Exception("CryptoPortfolioEmailPassword does not exist");
                
            }

            if(_username == null)
            {
                throw new Exception("CryptoPortfolioEmailUsername does not exist");
            }

            _client = new SmtpClient("smtp.gmail.com", 587);
            _client.EnableSsl = true;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential(_username, _password);
        }

        public void SendEmailAsync(NotificationEmail email, Notification notification, IUnitOfWork uow,
            Action handleSuccesfulEmailMethod = null, Action handleFailedEmailMethod = null)
        {
            if (handleFailedEmailMethod is null)
            {
                HandleFailedEmailDelegate = HandleFailedEmail;
            }
            if (handleSuccesfulEmailMethod is null)
            {
                HandleSuccessfulEmailDelegate = () => HandleSuccessfulEmail(notification, uow);
            }


            if (email.Sender == null)
                throw new ArgumentNullException(nameof(email.Sender));

            MailMessage message = new MailMessage();
            message.To.Add(email.Reciever);
            message.From = new MailAddress(email.Sender);
            message.Subject = email.Title;
            message.Body = email.Body;
            _client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            _client.SendAsync(message, message);
                
        }

        private void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
        {
            MailMessage email = (MailMessage)e.UserState;

            string emailSender = email.From.ToString();

            //if (e.Cancelled)
            //{
            //    //Console.WriteLine("[{0}] Send canceled.", token);
            //}
            if (e.Error != null)
            {
                HandleFailedEmailDelegate.Invoke();
                Console.WriteLine("error email");
            }
            else
            {
                HandleSuccessfulEmailDelegate.Invoke();
                Console.WriteLine("success email");
            }
        }


        /// <summary>
        /// Deletes notification from db after requirements are met.
        /// </summary>
        public void HandleSuccessfulEmail(Notification notification, IUnitOfWork uow)
        {
            uow.NotificationRepository.Remove(notification);
        }

        /// <summary>
        /// Method only for testing purpouse. Failed emails wait for another round of notification checking.
        /// </summary>
        public void HandleFailedEmail()
        {
        }
    }
}
