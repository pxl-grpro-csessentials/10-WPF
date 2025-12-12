using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Models
{
    public class Card
    {
        public int[] Value { get; set; }

        private string _imageUrl;
        public string ImageUrl
        {
            get 
            { 
                if(IsVisible)
                {
                    return _imageUrl; 
                }
                else
                {
                    return "images/cards/back.png";
                }
            }
            set { _imageUrl = value; }
        }

        public bool IsVisible { get; set; }

    }
}
