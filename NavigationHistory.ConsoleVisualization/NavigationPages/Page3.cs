using NavigationHistory.Lib;
using System;

namespace NavigationHistory.ConsoleVisualization.NavigationPages
{
    public class Page3 : BasePage, INavigationHistoryItem
    {
        public Page3() : base(3, ConsoleColor.Green)
        {
        }
    }
}