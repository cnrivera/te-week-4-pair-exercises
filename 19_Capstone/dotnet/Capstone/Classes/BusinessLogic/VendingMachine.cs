using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.BusinessLogic
{
    public class VendingMachine
    {
        /// <summary>
        /// <string, List<item>> -> <slot, item>
        /// </summary>
        private Dictionary<string, Item> Inventory = new Dictionary<string, Item>();
        public decimal CustomerBalance { get; private set; }
        public decimal VendingMachineBalance { get; private set; }
        public string[] Slots
        {
            get
            {
                List<string> keys = new List<string>();
                foreach(string key in Inventory.Keys)
                {
                    keys.Add(key);
                }
                return keys.ToArray();

            }
        }
        public VendingMachine()
        {
            CustomerBalance = 0;
        }

        public void AddNewItem(string slot, Item item)
        {
            Inventory[slot] = item;
        }

        public string[] GetInventory()
        {
            List<string> items = new List<string>();
            foreach (string slot in Inventory.Keys)
            {
                string itemInfo = $"{slot}: {Inventory[slot].Name} {Inventory[slot].Price.ToString("C")} ";
                if (Inventory[slot].AvailableCount == 0)
                {
                    itemInfo += "SOLD OUT";
                }
                items.Add(itemInfo);

            }

            return items.ToArray();
        }

        public void AddFunds(decimal funds)
        {
            CustomerBalance += funds;
        }

        public decimal GetItemPrice(string slot)
        {
            return Inventory[slot].Price;
        }

        public string BuyItem(string slot)
        {
            Item item = Inventory[slot];

            CustomerBalance -= item.Price;
            item.Purchase();

            return item.Message;
        }
    }
}
