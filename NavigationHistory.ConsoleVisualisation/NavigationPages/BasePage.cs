using NavigationHistory.Lib;
using System;

namespace NavigationHistory.ConsoleVisualisation.NavigationPages
{
    public class BasePage : INavigationHistoryItem
    {
        public BasePage(int pageNumber, ConsoleColor color)
        {
            PageNumber = pageNumber;
            Color = color;
        }

        public int PageNumber { get; }

        public ConsoleColor Color { get; }

        public string Identifier => PageNumber.ToString();
    }
}
