using NavigationHistory.Lib;
using System;

namespace NavigationHistory.ConsoleVisualisation.NavigationPages
{
    public class Page1 : BasePage, INavigationHistoryItem
    {
        public Page1() : base(1, ConsoleColor.Cyan)
        {
        }
    }
}