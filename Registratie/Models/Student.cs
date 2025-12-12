using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Registratie.Models
{
    public class Student
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public char Sex { get; set; }
        public List<Olod> Olods { get; set; }

        public Student()
        {
            this.Olods = new List<Olod>();
        }

        public void SubscribeTo(Olod olod)
        {
            this.Olods.Add(olod);
        }

        public string GetOlodSummary()
        {
            return string.Join(",", this.Olods.Select(o => o.Name));
        }

        private int GetTotalCredits()
        {
            return this.Olods.Sum(o => o.Credits);
        }

        public override string ToString()
        {
            return $"{this.Name} ({this.Sex})\r\n{this.GetTotalCredits()} studiepunten";
        }
    }
}
