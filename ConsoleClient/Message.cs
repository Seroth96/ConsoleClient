using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleClient
{
    public class Message
    {
        public string message { get; set; }

        public new string ToString()
        {
            return message;
        }
    }
}
