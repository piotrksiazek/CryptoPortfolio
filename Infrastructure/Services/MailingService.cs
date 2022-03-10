using Core.Interfaces.Services;
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
        private readonly string _username = "MyCryptoPortfolioApp@gmail.com";
        private string? _password;
        private readonly SmtpClient _client;

        public MailingService()
        {
            _password = Environment.GetEnvironmentVariable("CryptoPortfolioEmailPassword");
            if(_password == null)
            {
                throw new Exception("CryptoPortfolioEmailPassword does not exist");
                
            }

            _client = new SmtpClient("smtp.gmail.com", 587);
            _client.EnableSsl = true;
            _client.DeliveryMethod = SmtpDeliveryMethod.Network;
            _client.UseDefaultCredentials = false;
            _client.Credentials = new NetworkCredential(_username, _password);
        }

        public void SendEmail(string title, string body, string reciever)
        {
            MailMessage message = new MailMessage();
            message.To.Add(reciever);
            message.From = new MailAddress(_username);
            message.Subject = title;
            message.Body = body;
            _client.Send(message);
        }
    }
}
