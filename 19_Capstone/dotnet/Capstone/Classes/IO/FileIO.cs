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
                while (!sr.EndOfStream)
                {
                    // add line to the list
                    lines.Add(sr.ReadLine());
                }
            }

            return lines;
        }

        public static VendingMachine CreateVendingMachine(List<string> itemLines)
        {
            VendingMachine vendingMachine = new VendingMachine(false);
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

        public static void WriteToFile(string fileName, string fileContent, bool append)
        {
            try
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
                using (StreamWriter sw = new StreamWriter(fileName, append))
                {
                    sw.Write(fileContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public static List<string> GetAuditLog()
        {
            List<string> auditLogLines = new List<string>();
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Log.txt");
            using (StreamReader sr = new StreamReader(filePath))
            {
                while (!sr.EndOfStream)
                {
                    auditLogLines.Add(sr.ReadLine());
                }
            }
            return auditLogLines;
        }

        public static List<string> ReadSalesReport()
        {
            List<string> lines = new List<string>();

            using (StreamReader sr = new StreamReader(Path.Combine(Directory.GetCurrentDirectory(), "SalesReport.txt")))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
            }

            return lines;
        }

        public static void GetPreviousSales(VendingMachine vendingMachine)
        {
            if(File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "SalesReport.txt")))
            {
                List<string> lines = ReadSalesReport();
                Dictionary<string, int> itemsSold = new Dictionary<string, int>();
                decimal previousSales = 0;
                foreach (string line in lines)
                {
                    if (line.Contains("|"))
                    {
                        KeyValuePair<string, int> itemSold = InterpretSalesReportLine(line);
                        itemsSold[itemSold.Key] = itemSold.Value;
                    }
                    else if (line.Contains("**TOTAL SALES**"))
                    {
                        string sales = line.Substring(line.IndexOf("$") + 1);
                        previousSales = decimal.Parse(sales);
                    }
                }
                foreach (KeyValuePair<string, int> itemSold in itemsSold)
                {
                    if (itemSold.Value > 0)
                    {
                        vendingMachine.AddPreviousItemSoldCount(itemSold.Key, itemSold.Value);
                    }
                }

                vendingMachine.AddPreviousSales(previousSales);
            }
            
        }

        public static KeyValuePair<string, int> InterpretSalesReportLine (string line)
        {
            string[] splitResult = line.Split("|");
            string itemName = splitResult[0].Trim();
            string itemCount = splitResult[1].Trim();

            KeyValuePair<string, int> itemSold = new KeyValuePair<string, int>(itemName, int.Parse(itemCount));

            return itemSold;
        }

        public static void UpdateSalesReport(List<string> lines)
        {

        }
    }
}
