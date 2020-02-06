using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.BusinessLogic
{
    public class Drink : Item
    {
        public override string Message
        {
            get
            {
                return "Glug Glug, Yum!";
            }
        }

        public Drink(string name, decimal price)
        {
            Name = name;
            Price = price;
            AvailableCount = 5;
        }

    }
}
