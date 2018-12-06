using FluentAssertions;
using NavigationHistory.UnitTest.TestData;
using System;
using Xunit;

namespace NavigationHistory.UnitTest.NavigationHistoryTests
{
    public  class MaxHistorySizeTests : IDisposable
    {
        private Lib.NavigationHistory _navigationHistory;

        public MaxHistorySizeTests()
        {
            _navigationHistory = new Lib.NavigationHistory();
        }

        public void Dispose()
        {
            _navigationHistory = new Lib.NavigationHistory();
        }

        [Fact]
        public void ShouldRemoveOldHistoryItems_WhenHistoryCapacityHasBeenReached()
        {
            // given
            var navigationHistory = new Lib.NavigationHistory(3);

            // when
            navigationHistory.Record(TestNavigationItems.HomePage);
            navigationHistory.Record(TestNavigationItems.Page1);
            navigationHistory.Record(TestNavigationItems.Page2);
            navigationHistory.Record(TestNavigationItems.Page3);
            navigationHistory.Record(TestNavigationItems.Page4);

            // then
            navigationHistory.Back().Should().Be(TestNavigationItems.Page3);
            navigationHistory.Back().Should().Be(TestNavigationItems.Page2);
            navigationHistory.Back().Should().Be(TestNavigationItems.Page1);
            navigationHistory.Back().Should().BeNull();
        }

        [Fact]
        public void ShouldRemoveOldForwardHistoryItems_WhenHistoryCapacityHasBeenReached()
        {
            // given
            var navigationHistory = new Lib.NavigationHistory(3);

            // when
            navigationHistory.Record(TestNavigationItems.HomePage);
            navigationHistory.Record(TestNavigationItems.Page1);
            navigationHistory.Record(TestNavigationItems.Page2);
            navigationHistory.Record(TestNavigationItems.Page3);
            navigationHistory.Back();
            navigationHistory.Back();
            navigationHistory.Back();
            navigationHistory.Back();

            // then
            navigationHistory.Forward().Should().Be(TestNavigationItems.HomePage);
            navigationHistory.Forward().Should().Be(TestNavigationItems.Page1);
            navigationHistory.Forward().Should().Be(TestNavigationItems.Page2);
            navigationHistory.Forward().Should().BeNull();
        }
    }
}