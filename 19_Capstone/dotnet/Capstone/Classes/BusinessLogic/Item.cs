using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.BusinessLogic
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public decimal Price { get; protected set; }
        public int AvailableCount { get; protected set; }
        public int NumberSold { get; protected set; }
        public virtual string Message { get; }

        public void Purchase()
        {
            AvailableCount--;
            NumberSold++;
        }
    }
}
