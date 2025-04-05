using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Proyecto.Business;
using Proyecto.Data;

namespace Proyecto.Service
{
    public class MailService
    {
        public bool SendMail(string pdfPath)
        {          
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("jcastillor1104@gmail.com");
                mail.To.Add("jcastillori1104@outlook.com");
                mail.Subject = "Failed Tasks";
                mail.Body = "Attached you will find the file with the task list.";
                mail.Attachments.Add(new Attachment(pdfPath));
                mail.IsBodyHtml = false;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("jcastillor1104@gmail.com", "kuqx kutq mxdz pkwr");
                smtp.EnableSsl = true;
                smtp.Send(mail);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error Sending Mail: " + ex.Message);
            }
        }

    }
}
