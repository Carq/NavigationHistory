using FluentAssertions;
using System;
using Xunit;

namespace NavigationHistory.UnitTest
{
    public class NavigationHistoryUnitTests : IDisposable
    {
        private Lib.NavigationHistory _navigationHistory;

        public NavigationHistoryUnitTests()
        {
            _navigationHistory = new Lib.NavigationHistory();
        }

        public void Dispose()
        {
            _navigationHistory = new Lib.NavigationHistory();
        }

        [Fact]
        public void BackShouldReturnNull_WhenOnlyOneRecordedItemIsInHistory()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);

            // when
            var result = _navigationHistory.Back();

            // then
            result.Should().BeNull();
        }

        [Fact]
        public void BackShouldReturnFirstInsertedItem_WhenTwoRecordedItemsAreInHistory()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);

            // when
            var result = _navigationHistory.Back();

            // then
            result.Should().Be(TestNavigationItems.HomePage);
        }

        [Fact]
        public void SecondBackShouldReturnNull_WhenTwoRecordedItemsAreInHistory()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);

            // when
            var firstBackResult = _navigationHistory.Back();
            var secondBackResult = _navigationHistory.Back();

            // then
            secondBackResult.Should().BeNull();
        }

        [Fact]
        public void SecondBackShouldReturnSecondRecordedItem_WhenItemHasBeenRecorderAfterBackInvoke()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);
            _navigationHistory.Record(TestNavigationItems.Page2);

            // when
            var firstBackResult = _navigationHistory.Back();
            _navigationHistory.Record(TestNavigationItems.Page3);
            var secondBackResult = _navigationHistory.Back();

            // then
            secondBackResult.Should().Be(TestNavigationItems.Page1);
        }

        [Fact]
        public void FourBacksShouldReturnCorrectItems_WhenRecordAndBackAreInvokedAlternately()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);
            _navigationHistory.Record(TestNavigationItems.Page2);

            // when
            var firstBackResult = _navigationHistory.Back();
            _navigationHistory.Record(TestNavigationItems.Page3);
            _navigationHistory.Record(TestNavigationItems.Page4);
            var secondBackResult = _navigationHistory.Back();
            var thirdBackResult = _navigationHistory.Back();
            var fourthBackResult = _navigationHistory.Back();
            var fifthBackResult = _navigationHistory.Back();

            // then
            secondBackResult.Should().Be(TestNavigationItems.Page3);
            thirdBackResult.Should().Be(TestNavigationItems.Page1);
            fourthBackResult.Should().Be(TestNavigationItems.HomePage);
            fifthBackResult.Should().BeNull();
        }

        [Fact]
        public void ShouldNotAllowToBack_WhenPage1IsRecordedTwice()
        {
            // given
            _navigationHistory.Record(TestNavigationItems.HomePage);
            _navigationHistory.Record(TestNavigationItems.Page1);
            _navigationHistory.Record(TestNavigationItems.Page1);

            // then
            var firstBackResult = _navigationHistory.Back();
            var secondBackResult = _navigationHistory.Back();

            // when
            firstBackResult.Should().Be(TestNavigationItems.HomePage);
            secondBackResult.Should().BeNull();
        }

        [Fact]
        public void CanMoveBackShouldReturnFalse_WhenNoHistoryIsRecorded()
        {
            // given & when & then
            _navigationHistory.CanMoveBack().Should().BeFalse();
        }

        [Fact]
        public void BackShouldReturnNull_WhenNoHistoryIsRecorded()
        {
            // given & when & then
            _navigationHistory.Back().Should().BeNull();
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
    }
}
