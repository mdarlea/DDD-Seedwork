using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Configuration;

namespace Swaksoft.Infrastructure.Crosscutting.Authorization
{
    public class ApplicationUserMessageService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {

            var configSection = ConfigurationManager.GetSection("emailSection") as EmailSection;

            var emailConfig = configSection?.Login;
            if (emailConfig == null) return;

            using (var client = new SmtpClient())
            {
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                //client.Timeout = 10000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(emailConfig.User, emailConfig.Password);

                await client.SendMailAsync(emailConfig.User, message.Destination, message.Subject, message.Body);
            }
        }
    }
}