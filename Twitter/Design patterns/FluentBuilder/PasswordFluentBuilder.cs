using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twitter.Models;

namespace Twitter.Builder
{
    public class PasswordFluentBuilder
    {
        private readonly Password _password;

        public static PasswordFluentBuilder Create(DifficultyEnum difficulty)
        {
            return new PasswordFluentBuilder(difficulty);
        }

        private PasswordFluentBuilder(DifficultyEnum d)
        {
            _password = new Password { difficulty = d };
        }

        public Password Finish()
        {
            return _password;
        }
    }
}
