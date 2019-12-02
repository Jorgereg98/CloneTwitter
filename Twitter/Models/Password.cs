using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Password
    {
        public string code { get; set; }
        public DifficultyEnum difficulty { get; set; }

        public Password()
        {

        }

        public Password(DifficultyEnum difficulty, string code)
        {
            this.difficulty = difficulty;
            this.code = code;
        }

        public override string ToString()
        {
            return $"{difficulty} password";
        }
    }
}
