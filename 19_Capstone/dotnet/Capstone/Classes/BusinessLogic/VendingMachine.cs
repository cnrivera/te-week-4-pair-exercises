using Capstone.Classes.IO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;

namespace Capstone.Classes.BusinessLogic
{
    public class VendingMachine
    {
        /// <summary>
        /// <string, item> -> <slot, item>
        /// </summary>
        private Dictionary<string, Item> Inventory = new Dictionary<string, Item>();
        public List<Log> auditLog = new List<Log>();
        private bool inTestMode;
        public decimal CustomerBalance { get; private set; }
        public decimal VendingMachineBalance { get; private set; }
        public string[] Slots
        {
            get
            {
                List<string> keys = new List<string>();
                foreach (string key in Inventory.Keys)
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
                foreach (Item item in Inventory.Values)
                {
                    itemsSold[item.Name] = item.NumberSold;
                }
                return itemsSold;
            }
        }
        public VendingMachine(bool testMode)
        {
            inTestMode = testMode;
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
                string itemInfo = String.Format("{0,-25}{1,20:C}", slot + ": " + Inventory[slot].Name, Inventory[slot].Price);
                if (Inventory[slot].AvailableCount == 0)
                {
                    itemInfo += " SOLD OUT";
                }
                items.Add(itemInfo);

            }

            return items.ToArray();
        }

        public void AddFunds(decimal funds)
        {
            if (funds >= 0)
            {
                CustomerBalance += funds;
                AddToAuditLog("FEED MONEY", CustomerBalance, funds);
            }
        }

        public decimal GetItemPrice(string slot)
        {
            decimal price = 0;
            if (slot != null && Inventory.ContainsKey(slot))
            {
                price = Inventory[slot].Price;
            }
            return price;
        }
        public bool IsInStock(string slot)
        {
            bool inStock = false;
            if (slot != null)
            {
                slot = slot.ToUpper();
                if (Inventory.ContainsKey(slot))
                {
                    inStock = Inventory[slot].AvailableCount > 0;
                }
            }
            return inStock;
        }
        public string BuyItem(string slot)
        {
            string message = "";
            if (slot != null)
            {
                slot = slot.ToUpper();
                if (Inventory.ContainsKey(slot))
                {
                    Item item = Inventory[slot.ToUpper()];
                    if (item.AvailableCount > 0)
                    {
                        CustomerBalance -= item.Price;
                        VendingMachineBalance += item.Price;
                        item.Purchase();
                        if (inTestMode)
                        {
                            item.AddPreviouslySoldCount(-1);
                            UpdateSalesReport();
                        }
                        //auditLog.Add(new Log(Inventory[slot].Name, CustomerBalance, Inventory[slot].Price));
                        AddToAuditLog(Inventory[slot].Name, CustomerBalance, Inventory[slot].Price);
                        message = item.Message;
                    }
                }
            }

            return message;

        }

        public Dictionary<string, int> GiveChange()
        {
            Dictionary<string, int> coinCounts = new Dictionary<string, int>();
            // Change currency formatting to display negative sign rather than parenthesis 
            CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
            CultureInfo newCulture = new CultureInfo(currentCulture.Name);
            newCulture.NumberFormat.CurrencyNegativePattern = 1;
            Thread.CurrentThread.CurrentCulture = newCulture;
            coinCounts["Quarters"] = 0;
            coinCounts["Dimes"] = 0;
            coinCounts["Nickles"] = 0;
            decimal change = CustomerBalance;
            while (CustomerBalance > 0)
            {
                if (CustomerBalance >= .25M)
                {
                    CustomerBalance -= .25M;
                    coinCounts["Quarters"]++;
                }
                else if (CustomerBalance >= .10M)
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
            //auditLog.Add(new Log("GIVE CHANGE", CustomerBalance, -1 * change));
            AddToAuditLog("GIVE CHANGE", CustomerBalance, -1 * change);
            if (!inTestMode)
            {
                UpdateSalesReport();
            }
            return coinCounts;
        }

        public void AddToAuditLog(string action, decimal customerBalance, decimal changeInBalance)
        {
            if (!inTestMode)
            {
                Log newLog = new Log(action, CustomerBalance, changeInBalance);
                auditLog.Add(newLog);
                FileIO.WriteToFile("Log.txt", "\n" + newLog.ToString(), true);
            }            
        }

        public void AddPreviousSales(decimal sales)
        {
            VendingMachineBalance += sales;
        }

        public void AddPreviousItemSoldCount(string itemName, int soldCount)
        {
            foreach(Item item in Inventory.Values)
            {
                if(item.Name == itemName)
                {
                    item.AddPreviouslySoldCount(soldCount);
                }
            }
        }

        private void UpdateSalesReport()
        {
            decimal totalSales = VendingMachineBalance;
            string fileContent = "";
            foreach (Item item in Inventory.Values)
            {
                string line = $"{item.Name} | {item.NumberSold}";
                fileContent += line + "\n";
            }
            string sales = $"\n**TOTAL SALES** {totalSales.ToString("C")}";
            fileContent += sales;
            FileIO.WriteToFile("SalesReport.txt", fileContent, false);
        }
    }
}
