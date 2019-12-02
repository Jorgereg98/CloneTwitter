using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Strategy
{
    public class DomainValidation : IMailValidation
    {
        public bool Verification(string mail)
        {
            int count = 0;
            int pos = 0;
            for (int i = 0; i < mail.Length; i++)
            {
                if (mail[i] == '@')
                {
                    pos = i;
                }
            }

            for (int i = pos + 1; i < mail.Length; i++)
            {
                if (mail[i] == '.')
                {
                    count += 1;
                }
            }

            if (count != 1) return false;
            else return true;
        }
    }
}
