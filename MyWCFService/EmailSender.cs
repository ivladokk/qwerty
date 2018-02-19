using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFService
{
    public class EmailSender
    {
        private MailMessage mailMessage;
        private SmtpClient client;
        private IContentGenerator _contentGenerator;

        public EmailSender(string email, IContentGenerator contentGenerator)
        {
            this._contentGenerator = contentGenerator;
            mailMessage = new MailMessage("Reporting@test-project.com", email);
            mailMessage.Subject = "Report";
            mailMessage.Body = _contentGenerator.GetContent();
            client = new SmtpClient();
            client.Host = ConfigurationManager.AppSettings["EmailServiceHost"];
            client.Port = Convert.ToInt32(ConfigurationManager.AppSettings["EmailServicePort"]);
        }
        public void Send()
        {
            try
            {
                client.Send(mailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
