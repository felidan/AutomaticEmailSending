using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMail.Model
{
    public class DadosCredenciais
    {
        public DadosCredenciais()
        {
            this.EmailEnvio = "";
            this.PassWord = "";
        }
        public string EmailEnvio { get; set; }
        public string PassWord { get; set; }
    }
}
