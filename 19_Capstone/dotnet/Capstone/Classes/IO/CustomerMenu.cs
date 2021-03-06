﻿using Capstone.Classes.BusinessLogic;
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
            string response = Console.ReadLine().Trim().ToUpper();
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

        public void PickOption(string response)
        {
            if (response == "1")
            {
                Console.Clear();
                AddFunds();
            }
            else if (response == "2")
            {
                EndTransaction();
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
            Console.WriteLine("-----------------------");
            Console.WriteLine("Current Balance: " + vendingMachine.CustomerBalance.ToString("C"));
            Console.WriteLine("-----------------------\n");
        }
        public void ShowItems()
        {
            List<string> items = new List<string>(vendingMachine.GetInventory());
            string response;
            string[] menuOptions = { "1", "2", "3" };
            if (vendingMachine.CustomerBalance >= 0)
            {
                do
                {
                    Console.Clear();
                    DisplayBalance();
                    foreach (string item in items)
                    {
                        Console.WriteLine(item);
                    }

                    Console.WriteLine("\nEnter the item number you would like to purchase, or enter any of the following options:");
                    Console.WriteLine("1) Add Funds, 2) End Transaction 3) Return to Main Menu");
                    response = Console.ReadLine().Trim().ToUpper();
                    if (vendingMachine.Slots.Contains(response))
                    {
                        if (vendingMachine.IsInStock(response))
                        {
                            PurchaseItem(response);                                                                                        
                            items = new List<string>(vendingMachine.GetInventory());
                        }
                        else 
                        {
                            Console.WriteLine("Sorry, this item is out of stock. Press any button to continue");
                            Console.ReadKey();
                        }
                    }
                    //else if (response != "1")
                    //{
                    //    Console.WriteLine("Invalid item selected, please try again or enter 1 to return to the main menu.");
                    //    Console.ReadKey();
                    //}
                }
                while (!menuOptions.Contains(response));
                if(response != "3")
                {
                    PickOption(response);
                }
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
            while(funds <= 0)
            {
                Console.Clear();
                DisplayBalance();
                Console.WriteLine("How much would you like to deposit?");
                Console.WriteLine("1) $1.00\n2) $5.00\n3) $10.00");
                if(funds == -1)
                {
                    Console.WriteLine("Please enter a valid option (1, 2 or 3)");
                }
                response = Console.ReadLine().Trim().ToUpper();

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
                    funds = -1;
                }

            }

            return funds;
        }
        public bool PurchaseItem(string slot)
        {
            string message;
            bool returnToMainMenu = false;
            if(vendingMachine.CustomerBalance >= vendingMachine.GetItemPrice(slot))
            {
                message = vendingMachine.BuyItem(slot);
                Console.WriteLine(message);                
            }
            else
            {
                Console.WriteLine("Insufficent funds.");
            }
            //Console.WriteLine("Press any button to continue or 1 to return to the main menu.");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
            //response = Console.Read();
            //if (response == 49)
            //{
            //    returnToMainMenu = true;
            //}
            //Console.Clear();
            return returnToMainMenu;
        }

        public void EndTransaction()
        {
            Dictionary<string, int> coinCount = vendingMachine.GiveChange();
            Console.WriteLine($"Your change is {coinCount["Quarters"]} quarters, {coinCount["Dimes"]} dimes and {coinCount["Nickles"]} nickles");
            Console.WriteLine("Press any button to continue");
            Console.ReadKey();
        }
    }
}
