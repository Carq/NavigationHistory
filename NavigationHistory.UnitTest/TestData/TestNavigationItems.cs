using System;
using System.Collections.Generic;
using System.Text;

namespace NavigationHistory.UnitTest.TestData
{
    public static class TestNavigationItems
    {
        public static readonly TestNavigationItem HomePage = new TestNavigationItem(nameof(HomePage));

        public static readonly TestNavigationItem Page1 = new TestNavigationItem(nameof(Page1));

        public static readonly TestNavigationItem Page2 = new TestNavigationItem(nameof(Page2));

        public static readonly TestNavigationItem Page3 = new TestNavigationItem(nameof(Page3));

        public static readonly TestNavigationItem Page4 = new TestNavigationItem(nameof(Page4));
    }
}
