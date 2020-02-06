using System;
using System.IO;
using System.Collections.Generic;
using Capstone.Classes.IO;
using Capstone.Classes.BusinessLogic;
using Capstone.Classes;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine;
            List<string> lines = new List<string>();
            lines = FileIO.ReadVendingMachineFile();
            vendingMachine = FileIO.CreateVendingMachine(lines);
            IMenu menu = PickMenu(vendingMachine);
            RunMenu(menu,vendingMachine);            
        }
        static IMenu PickMenu(VendingMachine vendingMachine)
        {
            string ownerPin = "1234";
            string response;
            IMenu menu;

            Console.WriteLine("Welcome to Vendo-Matic 600. Press enter to continue.");
            response = Console.ReadLine();
            if(response == ownerPin)
            {
                menu = new OwnerMenu(vendingMachine);
            }
            else
            {
                menu = new CustomerMenu(vendingMachine);
            }
            return menu;
        }
        static void RunMenu(IMenu menu, VendingMachine vendingMachine)
        {
            while (menu.IsRunning)
            {
                menu.ShowOptions();
                menu.PickOption();

                if (menu is CustomerMenu)
                {
                    CustomerMenu customerMenu = (CustomerMenu)menu;
                    if (customerMenu.goToOwnerMenu)
                    {
                        menu = new OwnerMenu(vendingMachine);
                    }
                }
                else
                {
                    OwnerMenu ownerMenu = (OwnerMenu)menu;
                    if (ownerMenu.returnToCustomerMenu)
                    {
                        menu = new CustomerMenu(vendingMachine);
                    }
                }
            }
        }

        
    }
}
