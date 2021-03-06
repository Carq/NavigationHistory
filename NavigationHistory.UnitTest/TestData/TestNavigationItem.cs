﻿using NavigationHistory.Lib;

namespace NavigationHistory.UnitTest.TestData
{
    public class TestNavigationItem : INavigationHistoryItem
    {
        public TestNavigationItem(string identifier)
        {
            Identifier = identifier;
        }

        public string Identifier { get; }
    }
}