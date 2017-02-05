using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Token
    {
        public string access_token { get; set; }

        public new string ToString()
        {
            return access_token;
        }
    }

   
}
