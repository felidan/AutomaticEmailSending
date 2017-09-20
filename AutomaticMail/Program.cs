using AutomaticMail.Classes;
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

            Console.WriteLine("Preparando o Envio...");
            
            Console.Write("Resultado: ");

            email.MainBox();

            Console.WriteLine("Processo finalizado.");

            Console.ReadKey();
        }
    }
}
