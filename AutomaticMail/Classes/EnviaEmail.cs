using AutomaticMail.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticMail.Classes
{
    public class EnviaEmail
    {
        private EnviaEmailService service = new EnviaEmailService();
        private EmailModel email;

        public EnviaEmail()
        {
            this.email = new EmailModel();
        }

        public void MainBox()
        {
            service.Email(email);
        }
    }
}
