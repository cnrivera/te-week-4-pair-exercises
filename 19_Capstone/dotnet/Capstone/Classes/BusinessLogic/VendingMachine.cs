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
        public List<Log> auditLog = new List<Log>();
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

        public Dictionary<string, int> ItemsSold
        {
            get
            {
                Dictionary<string, int> itemsSold = new Dictionary<string, int>();
                foreach(Item item in Inventory.Values)
                {
                    itemsSold[item.Name] = item.NumberSold;
                }
                return itemsSold;
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
            auditLog.Add(new Log("FEED MONEY", CustomerBalance, funds));
        }

        public decimal GetItemPrice(string slot)
        {
            return Inventory[slot].Price;
        }
        public bool IsInStock(string slot)
        {
            return Inventory[slot].AvailableCount > 0;
        }
        public string BuyItem(string slot)
        {
            Item item = Inventory[slot];
            

            CustomerBalance -= item.Price;
            VendingMachineBalance += item.Price;
            item.Purchase();
            auditLog.Add(new Log(Inventory[slot].Name, CustomerBalance, Inventory[slot].Price));
            return item.Message;
        }

        public Dictionary<string,int> GiveChange()
        {
            Dictionary<string, int> coinCounts = new Dictionary<string, int>();
            coinCounts["Quarters"] = 0;
            coinCounts["Dimes"] = 0;
            coinCounts["Nickles"] = 0;
            decimal change = CustomerBalance;
            while(CustomerBalance > 0)
            {
                if(CustomerBalance >= .25M)
                {
                    CustomerBalance -= .25M;
                    coinCounts["Quarters"]++;
                }
                else if(CustomerBalance >= .10M)
                {
                    CustomerBalance -= .10M;
                    coinCounts["Dimes"]++;
                }
                else
                {
                    CustomerBalance -= .05M;
                    coinCounts["Nickles"]++;
                }                
            }
            auditLog.Add(new Log("GIVE CHANGE", CustomerBalance, change));
            return coinCounts;
        }
    }
}
