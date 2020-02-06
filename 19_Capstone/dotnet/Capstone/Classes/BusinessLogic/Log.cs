using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes.BusinessLogic
{
    public class Log
    {
        public DateTime TransactionDate { get; }
        public string Action { get; }
        public decimal CurrentBalance { get; }
        public decimal ChangeInBalance { get; }

        public Log (string action, decimal currentBalance, decimal changeInBalance)
        {
            TransactionDate = DateTime.Now;
            Action = action;
            CurrentBalance = currentBalance;
            ChangeInBalance = changeInBalance;
        }

        public override string ToString()
        {
            return String.Format("{0,-10}{1,20}{2,25}{3,30}", TransactionDate.ToString("MM/dd/yyyy hh:mm:ss tt"), Action, ChangeInBalance.ToString("C"), CurrentBalance.ToString("C"));
            //return $"{TransactionDate.ToString("MM/dd/yyyy hh:mm:ss tt")} {Action} {ChangeInBalance.ToString("C")} {CurrentBalance.ToString("C")}";
        }
    }
}
