﻿using NavigationHistory.Lib;
using System;

namespace NavigationHistory.ConsoleVisualisation.NavigationPages
{
    public class Page2 : BasePage, INavigationHistoryItem
    {
        public Page2() : base(2, ConsoleColor.DarkGreen)
        {
        }
    }
}