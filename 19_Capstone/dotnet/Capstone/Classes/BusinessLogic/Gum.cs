﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.BusinessLogic
{
    public class Gum : Item
    {
        public override string Message
        {
            get
            {
                return "Chew Chew, Yum!";
            }
        }

        public Gum(string name, decimal price)
        {
            Name = name;
            Price = price;
            AvailableCount = 5;
        }
    }
}
