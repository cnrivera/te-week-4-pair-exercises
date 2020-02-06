using System;
using System.IO;
using System.Collections.Generic;
using Capstone.Classes.IO;
using Capstone.Classes.BusinessLogic;

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
            CustomerMenu customerMenu = new CustomerMenu(vendingMachine);

            while (customerMenu.IsRunning)
            {
                customerMenu.ShowOptions();
                customerMenu.PickOption();
            }
            


        }
    }
}
