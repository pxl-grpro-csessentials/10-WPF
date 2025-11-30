using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Models
{
    internal class Registration
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Registration(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
