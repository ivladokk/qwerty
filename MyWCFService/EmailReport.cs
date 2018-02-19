using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWCFService
{
    public class EmailReport : IEmailReport
    {
        public void SendReport(string email)
        {
            EmailSender sender = new EmailSender(email, new ProjectsContentGenerator());
            sender.Send();
        }
    }
}
