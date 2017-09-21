using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMail.Model
{
    public class DadosRemetente
    {
        public DadosRemetente()
        {
            this.Email = "";
            this.DsNomeRemetente = "";
        }

        public string Email { get; set; }
        public string DsNomeRemetente { get; set; }
    }
}
