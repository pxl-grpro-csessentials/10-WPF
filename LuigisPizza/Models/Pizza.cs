using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuigisPizza.Models
{
    public class Pizza
    {
        private string _description;
        private float _price;
        private string _code;

        public string Description => _description;
        public float Price => _price;
        public string Code => _code;

        public Pizza(string code, string description, float Price)
        {
            _code = code;
            _description = description;
            _price = Price;
        }
    }
}
