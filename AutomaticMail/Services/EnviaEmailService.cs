using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using AutomaticMail.Classes;
using System.IO;
using AutomaticMail.Model;

namespace AutomaticMail.Services
{
    public class EnviaEmailService
    {
        SmtpClient client;

        public EnviaEmailService()
        {
            this.client = new SmtpClient();
        }

        public EmailData LoadDataDest(EmailData email)
        {

            #region ler-credenciais

            string _lineCred = "";

            StreamReader fileCred = new StreamReader(@"\_root.txt");

            try{
                while((_lineCred = fileCred.ReadLine()) != null)
                {
                    var _tempCred = _lineCred.Split(';');

                    if (_lineCred.Count() != 5)
                    {
                        throw new Exception("Erro ao ler credenciais de root: Parametros inválidos (" + _lineCred.Count() + ")");
                    }

                    /* + Layout
                     *      + Email de credencial
                     *      + Password
                     *      + HostName de origem
                     *      + Email de envio
                     *      + Nome de origem
                     */

                    email.Credenciais.EmailEnvio = _lineCred[0].ToString();
                    email.Credenciais.PassWord = _lineCred[1].ToString();
                    email.HostName = _lineCred[2].ToString();
                    email.Remetente.Email = _lineCred[3].ToString();
                    email.Remetente.DsNomeRemetente = _lineCred[4].ToString();
                    
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao ler credenciais de root: " + ex.Message);
            }

            fileCred.Close();

            #endregion ler-credenciais

            #region ler-destiinatarios

            int _countDest = 0;
            string _lineDest = "";

            StreamReader fileDest = new StreamReader(@"\_EmailDest.txt");
            try
            {
                while ((_lineDest = fileDest.ReadLine()) != null)
                {
                    Destinatarios dest = new Destinatarios();

                    _countDest++;

                    dest.Email = _lineDest.ToString();
                    dest.IdEmail = _countDest;

                    email.Destinatarios.Add(dest);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao ler arquivo: " + "(_EmailDest.txt)" + ex.Message);
            }

            fileDest.Close();

            #endregion ler-destiinatarios

            #region ler_destinatarios-copia

            int _countDestCopy = 0;
            string _lineDestCopy = "";

            StreamReader fileDestCopy = new StreamReader(@"\__EmailDestCopy.txt");

            try
            {
                while((_lineDestCopy = fileDestCopy.ReadLine()) != null)
                {
                    Destinatarios destCopy = new Destinatarios();

                    _countDestCopy++;

                    destCopy.Email = _lineDestCopy.ToString();
                    destCopy.IdEmail = _countDestCopy;

                    email.DestinatariosCopia.Add(destCopy);
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao ler arquivo: " + "(_EmailDestCopy.txt)" + ex.Message);
            }

            fileDestCopy.Close();

            #endregion ler_destinatarios-copia

            #region ler-titulo-body
            #endregion ler-titulo-body

            return email;
        }

        public void SendEmail()
        {
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("alex.event.teixeira@gmail.com", "@admin123");
            
            MailMessage mail = new MailMessage();
            mail.Sender = new MailAddress("alex.event.teixeira@gmail.com", "Alex Teste");
            mail.From = new MailAddress("alex.event.teixeira@gmail.com", "Eventos");

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