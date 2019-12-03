using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Strategy
{
    public class MailValidation
    {
        private readonly IMailValidation mailValidation;
        private readonly string mail;

        public MailValidation(IMailValidation mailValidation, string mail)
        {
            this.mailValidation = mailValidation;
            this.mail = mail;
        }

        public bool Verification()
        {
            return mailValidation.Verification(mail);
        }
    }
}
