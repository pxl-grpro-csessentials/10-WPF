using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registratie.Models
{
    public class Olod
    {
        public string Name { get; set; }
        public int Credits { get; set; }

        public override string ToString()
        {
            return $"{this.Name} ({this.Credits})";
        }
    }
}
