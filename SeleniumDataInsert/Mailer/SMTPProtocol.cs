using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumDataInsert.Mailer
{
    public static class SMTPProtocol
    {
        public static void NotifyPartners(string subject, string body, string emailTo)
        {

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("ramjinboss77@gmail.com");
                mail.To.Add(emailTo);
                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

                //mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                //mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("ramjinboss77@gmail.com", "9790030814");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }

 
        }
    }
}
