using AutomaticMail.Classes;
using AutomaticMail.Model;
using AutomaticMail.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMail
{
    class Program
    {
        static void Main(string[] args)
        {
            EnviaEmail email = new EnviaEmail();

            EnviaEmailService teste = new EnviaEmailService();
            EmailData data = new EmailData();

            teste.LoadData(data);

            foreach(Destinatarios item in data.Destinatarios) {
                Console.WriteLine(item.Email + "---" + item.IdEmail);
            }

            Console.WriteLine(data.Destinatarios.Count);

            /*
            Console.WriteLine("Preparando o Envio...");
            
            Console.Write("Resultado: ");

            email.MainBox();

            Console.WriteLine("Processo finalizado.");

            Console.ReadKey();
            */
        }
    }
}
