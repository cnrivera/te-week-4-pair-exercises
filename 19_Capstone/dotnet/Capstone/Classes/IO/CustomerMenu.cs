using Capstone.Classes.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace Capstone.Classes.IO
{
    public class CustomerMenu : IMenu
    {
        private bool isRunning;
        public bool goToOwnerMenu { get; private set; }
        public bool IsRunning
        {
            get
            {
                return isRunning;
            }
        }
        private VendingMachine vendingMachine;
        public CustomerMenu(VendingMachine theVendingMachine)
        {
            isRunning = true;
            goToOwnerMenu = false;
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
                Console.Clear();
                AddFunds();
            }
            else if(response == "2")
            {
                Console.Clear();
                ShowItems();
            }
            else if(response == "3")
            {
                EndTransaction();
            }
            else if(response == "4")
            {
                ExitMenu();
            }
            else if(response == "1234")
            {
                goToOwnerMenu = true;
                ExitMenu();
            }
        }

        public void ShowOptions()
        {
            Console.Clear();
            DisplayBalance();
            Console.WriteLine("1) Add Funds.\n2) See Items.\n3) End Transaction.\n4) Exit.");
        }
        public void DisplayBalance()
        {
            Console.WriteLine("Current Balance: " + vendingMachine.CustomerBalance.ToString("C"));
        }
        public void ShowItems()
        {
            List<string> items = new List<string>(vendingMachine.GetInventory());
            string response;
            if (vendingMachine.CustomerBalance > 0)
            {
                do
                {
                    Console.Clear();
                    DisplayBalance();
                    foreach (string item in items)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine("\nEnter the item number you would like to purchase, or 1 to return to the main menu");
                    response = Console.ReadLine();
                    if (vendingMachine.Slots.Contains(response))
                    {
                        if (vendingMachine.IsInStock(response))
                        {
                            if (PurchaseItem(response))
                            {
                                response = "1";
                            }
                            items = new List<string>(vendingMachine.GetInventory());
                        }
                        else
                        {
                            Console.WriteLine("Sorry, this item is out of stock. Press any button to continue");
                            Console.ReadKey();
                        }
                    }
                }
                while (response != "1");
                
            }
            else
            {
                Console.WriteLine("Please deposit money before choosing an item");
            }            
        }

        public void AddFunds()
        {
            // ask how much money to deposit
            decimal funds = FundsRequest();
            // add to balance
            vendingMachine.AddFunds(funds);
        }

        public decimal FundsRequest()
        {
            decimal funds = 0;
            string response;
            while(funds == 0)
            {
                Console.WriteLine("How much would you like to deposit?");
                Console.WriteLine("1) $1.00\n2) $5.00\n3) $10.00");
                response = Console.ReadLine();

                if(response == "1")
                {
                    funds = 1.00M;
                }
                else if(response == "2")
                {
                    funds = 5.00M;
                }
                else if(response == "3")
                {
                    funds = 10.00M;
                }
                else
                {
                    Console.WriteLine("Please enter a valid option (1, 2 or 3)");
                }

            }

            return funds;
        }
        public bool PurchaseItem(string slot)
        {
            string message;
            bool returnToMainMenu = false;
            int response;
            if(vendingMachine.CustomerBalance >= vendingMachine.GetItemPrice(slot))
            {
                message = vendingMachine.BuyItem(slot);
                Console.WriteLine(message);                
            }
            else
            {
                Console.WriteLine("Insufficent funds.");
            }
            Console.WriteLine("Press any button to continue or 1 to return to the main menu.");
            response = Console.Read();
            if (response == 49)
            {
                returnToMainMenu = true;
            }
            Console.Clear();
            return returnToMainMenu;
        }

        public void EndTransaction()
        {
            Dictionary<string, int> cointCount = vendingMachine.GiveChange();
            Console.WriteLine($"Your change is {cointCount["Quarters"]} quarters, {cointCount["Dimes"]} dimes and {cointCount["Nickles"]} nickles");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
    }
}
