using AutomaticMail.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMail.Classes
{
    public class EmailData
    {
        public EmailData()
        {
            this.Destinatarios = new List<Destinatarios>();
            this.DestinatariosCopia = new List<Destinatarios>();
            //this.Anexo = new List<string>();
        }
        //_root.txt
        public DadosCredenciais Credenciais { get; set; }
        public string HostName { get; set; }
        public DadosRemetente Remetente { get; set; }

        public List<Destinatarios> Destinatarios { get; set; }
        public List<Destinatarios> DestinatariosCopia { get; set; }
        //public List<string> Anexo { get; set; }
        public string Titulo { get; set; }
        public string BodyMail { get; set; }
    }
}
