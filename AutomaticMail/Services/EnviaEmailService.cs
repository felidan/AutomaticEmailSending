using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace AutomaticMail.Services
{
    public class EnviaEmailService
    {
        SmtpClient client;

        public EnviaEmailService()
        {
            this.client = new SmtpClient();
        }

        public void SendEmail()
        {
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("alex.event.teixeira@gmail.com", "@admin123");

            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress("alex.event.teixeira@gmail.com", "Alex Teste");
            mail.From = new MailAddress("alex.event.teixeira@gmail.com");

            mail.To.Add(new MailAddress("felipelipe1927@hotmail.com"));
            mail.To.Add(new MailAddress("felipesimplicio27@gmail.com"));

            mail.Subject = "Primeiro teste de envio de email";

            mail.Body = "Mensagem do site:< br />" 
                + "Nome:  " + "Nome de teste" + " < br />" 
                + "Email : " + "Email do teste" + " < br />" 
                + "Mensagem : " + "Olaaaa!";

            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            try
            {
                client.Send(mail);
                Console.WriteLine("Email enviado com sucesso!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex);
            }
            finally
            {
                mail = null;
            }
        }
    }
}