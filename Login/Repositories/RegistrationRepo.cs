using Login.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Login.Repositories
{
    internal static class RegistrationRepo
    {
        private static Dictionary<string, string> _registrations = new Dictionary<string, string>();

        public static Dictionary<string, string> Registrations
        {
            get { return _registrations; }
            set { _registrations = value; }
        }

        //public static Dictionary<string, string> Registrations =>
        //    new Dictionary<string, string>()
        //    {
        //        { "admin",   "A6xnQhbz4Vx2HuGl4lXwZ5U2I8iziLRFnhP5eNfIRvQ=" }, // 1234
        //        { "student", "fKWnI44os5F2VRrbaKqLvYgL9krK2Ig0V6IVMw2hNyM=" }, // passwoord
        //        { "john",     "e8oCXv9PuHe+qG+qxWyQm2XJet+6I7fB+2uXctNLQg4=" }, // Welkom123
        //        { "jane",     "ZehL4zUy+3hMSBKWdfnv86aCsnFowOp0Syz1juAjN8U=" }  // qwerty
        //    };
    }
}
