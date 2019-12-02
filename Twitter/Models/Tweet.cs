using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class Tweet
    {
        public string text { get; set; }
        public DateTime date { get; set; }
        public int likes { get; set; }
        public int idUser { get; set; }
    }
}
