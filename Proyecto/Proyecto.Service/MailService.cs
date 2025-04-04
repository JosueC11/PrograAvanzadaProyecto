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
        private readonly TareaManager _manager;
        public MailService()
        {
            _manager = new TareaManager();
        }

        public IEnumerable<tarea> GetAllFailedTasks()
        {
            return _manager.GetAllFailedTasks().ToList().OrderBy(n => n.prioridad);
        }
        public bool SendMail()
        {
            var tasks = GetAllFailedTasks();
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("jcastillor1104@gmail.com");
                mail.To.Add("jcastillori1104@outlook.com");
                mail.Subject = "Failed Tasks";

                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Lista de tareas fallidas:");
                foreach (var task in tasks)
                {
                    sb.AppendLine($"ID: {task.id}, Nombre: {task.nombre}, Prioridad: {task.prioridad}");
                }

                string bodyMessage = sb.ToString();

                mail.Body = bodyMessage;
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
