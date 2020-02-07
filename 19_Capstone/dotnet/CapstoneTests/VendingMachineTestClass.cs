using Capstone.Classes.BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;


namespace CapstoneTests
{
    [TestClass]
    public class VendingMachineTestClass
    {
        public VendingMachine vendingMachine;

        [TestInitialize]
        public void Initialize()
        {
            vendingMachine = new VendingMachine(true);
            Candy someCandy = new Candy("Candy", 1);
            Drink someDrink = new Drink("Drink", 1);
            Chips someChips = new Chips("Chips", 1);
            Gum someGum = new Gum("Gum", 1);

            vendingMachine.AddNewItem("A1", someCandy);
            vendingMachine.AddNewItem("B1", someDrink);
            vendingMachine.AddNewItem("C1", someChips);
            vendingMachine.AddNewItem("D1", someGum);
        }

        [TestMethod]
        public void TestDeposit()
        {
            vendingMachine.AddFunds(10);
            Assert.AreEqual(10, vendingMachine.CustomerBalance);

            Initialize();
            vendingMachine.AddFunds(-10);
            Assert.AreEqual(0, vendingMachine.CustomerBalance);
        }

        [TestMethod]
        public void TestGetPrice()
        {
            Assert.AreEqual(1.00M, vendingMachine.GetItemPrice("A1"));
            Assert.AreEqual(1.00M, vendingMachine.GetItemPrice("a1"));
            Assert.AreEqual(0, vendingMachine.GetItemPrice("Does not exist"));
            Assert.AreEqual(0, vendingMachine.GetItemPrice(null));
        }

        [TestMethod]
        public void TestIsInStock()
        {
            Initialize();
            Assert.AreEqual(true, vendingMachine.IsInStock("A1"));
            Assert.AreEqual(true, vendingMachine.IsInStock("a1"));

            vendingMachine.BuyItem("A1");
            vendingMachine.BuyItem("A1");
            vendingMachine.BuyItem("A1");
            vendingMachine.BuyItem("A1");
            vendingMachine.BuyItem("A1");

            Assert.AreEqual(false, vendingMachine.IsInStock("A1"));
            Assert.AreEqual(false, vendingMachine.IsInStock("a1"));

            Assert.AreEqual(false, vendingMachine.IsInStock("Does not exist"));
            Assert.AreEqual(false, vendingMachine.IsInStock(null));
        }

        [TestMethod]
        public void TestBuyItem()
        {
            Initialize();
            string gum = "Chew Chew, Yum!";
            string drink = "Glug Glug, Yum!";
            string chips = "Crunch Crunch, Yum!";
            string candy = "Munch Munch, Yum!";

            Assert.AreEqual(candy, vendingMachine.BuyItem("A1"));
            Assert.AreEqual(drink, vendingMachine.BuyItem("B1"));
            Assert.AreEqual(chips, vendingMachine.BuyItem("C1"));
            Assert.AreEqual(gum, vendingMachine.BuyItem("D1"));

            Assert.AreEqual(candy, vendingMachine.BuyItem("a1"));
            Assert.AreEqual(drink, vendingMachine.BuyItem("b1"));
            Assert.AreEqual(chips, vendingMachine.BuyItem("c1"));
            Assert.AreEqual(gum, vendingMachine.BuyItem("d1"));

            Assert.AreEqual("", vendingMachine.BuyItem("does not exist"));
            Assert.AreEqual("", vendingMachine.BuyItem(null));

            vendingMachine.BuyItem("A1");
            vendingMachine.BuyItem("A1");
            vendingMachine.BuyItem("A1");

            Assert.AreEqual("", vendingMachine.BuyItem("A1"));
        }
    }
}
