using Core.Entities;
using Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MailingService : IMailingService
    {
        private readonly string? _username;
        private string? _password;
        private readonly SmtpClient _client;
        private readonly IConfiguration _config;

        public MailingService(IConfiguration config)
        {
            _config = config;
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

        public void SendEmail(NotificationEmail email)
        {
            if (email.Sender == null)
                throw new ArgumentNullException(nameof(email.Sender));

            MailMessage message = new MailMessage();
            message.To.Add(email.Reciever);
            message.From = new MailAddress(email.Sender);
            message.Subject = email.Title;
            message.Body = email.Body;
            _client.Send(message);
                
        }
    }
}
