using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using SendGrid;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Configuration;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization
{
    public class EmailService : IIdentityMessageService
    {

        public async Task SendAsync(IdentityMessage message)
        {
            await ConfigSendGridasync(message);
        }

        // Use NuGet to install SendGrid (Basic C# client lib) 
        private async Task ConfigSendGridasync(IdentityMessage message)
        {
            var config = (EmailSection)ConfigurationManager.GetSection("emailSection");

            var myMessage = new SendGridMessage();

            myMessage.AddTo(message.Destination);
            myMessage.From = new MailAddress(config.Email.Address, config.Email.Name);
            myMessage.Subject = message.Subject;
            myMessage.Text = message.Body;
            myMessage.Html = message.Body;

            var credentials = new NetworkCredential(config.Login.User, config.Login.Password);

            // Create a Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email.
            await transportWeb.DeliverAsync(myMessage);
        }
    }
}
