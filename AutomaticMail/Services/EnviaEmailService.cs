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

        public void Email(EmailModel email)
        {
            LoadData(email);

            if (!ValidaDados(email))
            {
                throw new Exception("Existem dados inválidos!");
            }

            SendEmail(email);

        }

        public EmailModel LoadData(EmailModel email)
        {
            
            #region ler-credenciais

            string _lineCred = "";

            StreamReader fileCred = new StreamReader("_root.txt");

            try{
                while((_lineCred = fileCred.ReadLine()) != null)
                {
                    var _tempCred = _lineCred.Split(';');

                    if (_tempCred.Length != 5)
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

           email.Credenciais.EmailEnvio = _tempCred[0].ToString();
           email.Credenciais.Password = _tempCred[1].ToString();
           email.HostName = _tempCred[2].ToString();
           email.Remetente.Email = _tempCred[3].ToString();
           email.Remetente.DsNomeRemetente = _tempCred[4].ToString();

       }
   }
   catch(Exception ex)
   {
       throw new Exception("Erro ao ler credenciais de root: " + ex.Message);
   }

   fileCred.Close();

   #endregion ler-credenciais

            #region ler-destinatarios

            int _countDest = 0;
            string _lineDest = "";

            StreamReader fileDest = new StreamReader("_EmailDest.txt");
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
            
            #endregion ler-destinatarios
            
            #region ler_destinatarios-copia

            int _countDestCopy = 0;
            string _lineDestCopy = "";

            StreamReader fileDestCopy = new StreamReader("_EmailDestCopy.txt");

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
                throw new Exception("Erro ao ler arquivo: (_EmailDestCopy.txt)" + ex.Message);
            }

            fileDestCopy.Close();

            #endregion ler_destinatarios-copia

            #region ler-titulo-body

            string _lineTitleBody = "";
            int _aux = 0;
            
            StreamReader fileTitleBody = new StreamReader("_TitleBody.txt");

            try
            {
                while((_lineTitleBody = fileTitleBody.ReadLine()) != null)
                {
                    if(_aux == 0)
                    {
                        _aux = 1;
                        email.Titulo = _lineTitleBody.ToString();
                    }
                    else
                    {
                        email.BodyMail.Add(_lineTitleBody.ToString());
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao ler arquivo: (_TitleBody.txt) " + ex.Message);
            }

            #endregion ler-titulo-body

            #region ler-anexos

            int _countAnexo = 0;
            string _lineAnexo = "";

            StreamReader fileAnexo = new StreamReader("_anexo.txt");

            try
            {
                while((_lineAnexo = fileAnexo.ReadLine()) != null)
                {
                    AnexoModel tempAnexo = new AnexoModel();

                    _countAnexo++;

                    tempAnexo.CaminhoAnexo = _lineAnexo.ToString();
                    tempAnexo.ContAnexo = _countAnexo;

                    email.Anexo.Add(tempAnexo); 
                }
            }
            catch(Exception ex)
            {
                throw new Exception("Erro ao ler arquivo: (_anexo.txt) " + ex.Message);
            }
            fileAnexo.Close();

            #endregion ler-anexos
    
            return email;
        }

        public void SendEmail(EmailModel email)
        {
            client.Host = email.HostName;
            client.EnableSsl = true;

            client.Credentials = new NetworkCredential(
                email.Credenciais.EmailEnvio, 
                email.Credenciais.Password
                );

            MailMessage mail = new MailMessage();

            mail.Sender = new MailAddress(
                email.Remetente.Email, 
                email.Remetente.DsNomeRemetente
                );

            mail.From = new MailAddress(
                email.Remetente.Email,
                email.Remetente.DsNomeRemetente
                );
            
            foreach(Destinatarios item in email.Destinatarios)
            {
                mail.To.Add(new MailAddress(item.Email));
            }

            mail.Subject = email.Titulo;

            foreach(string item in email.BodyMail)
            {
                mail.Body += item;
            }
            
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            /*
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
            */
            try
            {
                client.Send(mail);
                Console.WriteLine("Email enviado com sucesso!");
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erro: " + ex.Message);
            }
            finally
            {
                mail = null;
            }
        }

        internal bool ValidaDados(EmailModel email)
        {
            bool resultado = true;

            #region valida-dados

            if (email.Credenciais.EmailEnvio.Equals("") || email.Credenciais.EmailEnvio == null)
            {
                return false;
            }
            if (email.Credenciais.Password.Equals("") || email.Credenciais.Password == null)
            {
                return false;
            }
            if (email.Destinatarios.Count() <= 0)
            {
                return false;
            }
            if (email.BodyMail.Count() <= 0)
            {
                return false;
            }
            if (email.HostName.Equals("") || email.HostName == null)
            {
                return false;
            }
            if (email.Titulo == null || email.Titulo.Equals(""))
            {
                return false;
            }

            #endregion valida-dados
            
            return resultado;
        }
    }
}