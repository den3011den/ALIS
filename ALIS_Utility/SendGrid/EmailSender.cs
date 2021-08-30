using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace ALIS_Utility.SendGrid
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailOptions emailOptions;
        public IConfiguration _configuration { get; }

        public EmailSender(IOptions<EmailOptions> options, IConfiguration configuration)
        {
            _configuration = configuration;
            emailOptions = options.Value;
            emailOptions.SendGridKey = StringEncryptionUtility.StringEncryptionUtility.Decrypt(emailOptions.SendGridKey);
            emailOptions.SendGridUser = StringEncryptionUtility.StringEncryptionUtility.Decrypt(emailOptions.SendGridUser);
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(emailOptions.SendGridKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendGridKey, string subject, string message, string email)
        {
             
            var client = new SendGridClient(sendGridKey);      
            var from = new EmailAddress(StringEncryptionUtility.StringEncryptionUtility.Decrypt(_configuration.GetValue<string>("SendGridEmailFrom")), "ALIS");
            var to = new EmailAddress(email, email);                        
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            return client.SendEmailAsync(msg);
        }
    }
}
