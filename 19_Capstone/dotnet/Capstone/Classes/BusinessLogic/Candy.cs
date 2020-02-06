using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.BusinessLogic
{
    public class Candy : Item
    {
        public override string Message
        {
            get
            {
                return "Munch Munch, Yum!";
            }
        }

        public Candy(string name, decimal price)
        {
            Name = name;
            Price = price;
            AvailableCount = 5;
        }
    }
}
