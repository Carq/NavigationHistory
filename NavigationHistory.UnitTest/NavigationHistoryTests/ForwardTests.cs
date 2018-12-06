using FluentAssertions;
using NavigationHistory.UnitTest.TestData;
using System;
using Xunit;

namespace NavigationHistory.UnitTest.NavigationHistoryTests
{
    public class ForwardTests : IDisposable
    {
        private Lib.NavigationHistory<TestNavigationItem> _navigationHistory;

        public ForwardTests()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        public void Dispose()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        [Fact]
        public void ShouldReturnLastRecordedItem_WhenOnlyOneBackWasInvoked()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);

            // when
            _navigationHistory.Back();

            // then
            _navigationHistory.Forward().Should().Be(TestNavigationItems.Page1);
            _navigationHistory.Forward().Should().BeNull();
        }

        [Fact]
        public void ShouldResetForwardHistory_WhenRecordHasBeenInvokedAfterBack()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);

            // when
            _navigationHistory.Back();
            _navigationHistory.Record(TestNavigationItems.Page2);

            // then
            _navigationHistory.Forward().Should().BeNull();
        }

        [Fact]
        public void ShouldReturnCorrectHistoryItems_WhenBackAndForwardAreInvokedAlternately()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);
            _navigationHistory.Record(TestNavigationItems.Page2);

            // when & then
            _navigationHistory.Back().Should().Be(TestNavigationItems.Page1);
            _navigationHistory.Forward().Should().Be(TestNavigationItems.Page2);
            _navigationHistory.Back().Should().Be(TestNavigationItems.Page1);
            _navigationHistory.Forward().Should().Be(TestNavigationItems.Page2);
        }

        [Fact]
        public void ShouldReturnHomePage_WhenGoBackIsNotPossible()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);

            // when
            _navigationHistory.Back();
            _navigationHistory.Back();

            // then
            _navigationHistory.Forward().Should().Be(TestNavigationItems.HomePage);
        }
    }
}