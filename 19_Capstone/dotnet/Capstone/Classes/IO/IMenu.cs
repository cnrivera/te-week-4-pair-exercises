using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public interface IMenu
    {
        bool IsRunning { get; }
        void ShowOptions();
        void PickOption();
        void ExitMenu();
    }
}
