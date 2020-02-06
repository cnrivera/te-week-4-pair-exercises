using Capstone.Classes.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.IO
{
    public class OwnerMenu : IMenu
    {
        private bool isRunning;
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }
        private VendingMachine vendingMachine;
        public OwnerMenu(VendingMachine theVendingMachine)
        {
            isRunning = true;
            vendingMachine = theVendingMachine;
        }
        public void ExitMenu()
        {
            isRunning = false;
        }

        public void PickOption()
        {
            string response = Console.ReadLine();
            if(response == "1")
            {
                ShowAudit();
            }
            else if(response == "2")
            {
                ShowSalesReport();
            }
            else if(response == "3")
            {
                ExitMenu();
            }
        }

        public void ShowOptions()
        {
            Console.Clear();
            Console.WriteLine("1) View audit\n2) View sales report\n3) Exit");
        }

        public void ShowAudit()
        {
            Console.Clear();
            foreach(Log line in vendingMachine.auditLog)
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();            
        }

        public void ShowSalesReport()
        {
            Console.Clear();
            decimal totalSales = vendingMachine.VendingMachineBalance;
            Dictionary<string, int> itemsSold = vendingMachine.ItemsSold;

            foreach(string itemName in itemsSold.Keys)
            {
                Console.WriteLine($"{itemName} | {itemsSold[itemName]}");
            }
            Console.WriteLine($"**TOTAL SALES** {totalSales.ToString("C")}");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
    }
}
