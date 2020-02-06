using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Capstone.Classes.BusinessLogic;

namespace Capstone.Classes.IO
{
    public static class FileIO
    {
        /// <summary>
        /// Reads the vending machine file
        /// </summary>
        /// <returns>A list containing each line from the file</returns>
        public static List<string> ReadVendingMachineFile()
        {
            List<string> lines = new List<string>();
            string filePath = Directory.GetCurrentDirectory() + "/Input Files/VendingMachine.txt";
            using (StreamReader sr = new StreamReader(filePath))
            {
                // while there are lines in the file
                while(!sr.EndOfStream)
                {
                    // add line to the list
                    lines.Add(sr.ReadLine());
                }
            }

            return lines;
        }

        public static VendingMachine CreateVendingMachine(List<string> itemLines)
        {
            VendingMachine vendingMachine = new VendingMachine();
            foreach (string line in itemLines)
            {
                InterpretLine(line, vendingMachine);
            }
            return vendingMachine;
        }

        public static void InterpretLine(string line, VendingMachine vendingMachine)
        {
            string[] lineSections = line.Split("|");
            string slot = lineSections[0];
            string itemName = lineSections[1];
            decimal price = Decimal.Parse(lineSections[2]);
            string itemType = lineSections[3];

            Item newItem = GetItem(itemName, price, itemType);

            vendingMachine.AddNewItem(slot, newItem);
            
        }

        public static Item GetItem(string itemName, decimal price, string itemType)
        {
            Item newItem;
            if (itemType == "Gum")
            {
                newItem = new Gum(itemName, price);
            }
            else if (itemType == "Candy")
            {
                newItem = new Candy(itemName, price);
            }
            else if (itemType == "Drink")
            {
                newItem = new Drink(itemName, price);
            }
            else if (itemType == "Chip")
            {
                newItem = new Chips(itemName, price);
            }
            else
            {
                newItem = null;
            }

            return newItem;
        }
    }
}
