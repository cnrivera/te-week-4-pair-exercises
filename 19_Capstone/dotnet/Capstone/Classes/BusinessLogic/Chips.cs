using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.BusinessLogic
{
    public class Chips : Item
    {
        public override string Message
        {
            get
            {
                return "Crunch Crunch, Yum!";
            }
        }

        public Chips(string name, decimal price)
        {
            Name = name;
            Price = price;
            AvailableCount = 5;
        }
    }
}
