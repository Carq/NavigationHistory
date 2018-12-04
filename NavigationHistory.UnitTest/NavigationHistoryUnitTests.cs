using FluentAssertions;
using System;
using Xunit;

namespace NavigationHistory.UnitTest
{
    public class NavigationHistoryUnitTests : IDisposable
    {
        private Lib.NavigationHistory _navigatoryHistory;

        public NavigationHistoryUnitTests()
        {
            _navigatoryHistory = new Lib.NavigationHistory();
        }

        public void Dispose()
        {
            _navigatoryHistory = new Lib.NavigationHistory();
        }

        [Fact]
        public void BackShouldReturnNull_WhenOnlyOneRecordedItemIsInHistory()
        {
            // given
            _navigatoryHistory.Record(TestNavigationItems.HomePage);

            // when
            var result = _navigatoryHistory.Back();

            // then
            result.Should().BeNull();
        }

        [Fact]
        public void BackShouldReturnFirstInsertedItem_WhenTwoRecordedItemsAreInHistory()
        {
            // given
            _navigatoryHistory.Record(TestNavigationItems.HomePage);
            _navigatoryHistory.Record(TestNavigationItems.Page1);

            // when
            var result = _navigatoryHistory.Back();

            // then
            result.Should().Be(TestNavigationItems.HomePage);
        }

        [Fact]
        public void SecondBackShouldReturnNull_WhenTwoRecordedItemsAreInHistory()
        {
            // given
            _navigatoryHistory.Record(TestNavigationItems.HomePage);
            _navigatoryHistory.Record(TestNavigationItems.Page1);

            // when
            var firstBackResult = _navigatoryHistory.Back();
            var secondBackResult = _navigatoryHistory.Back();

            // then
            secondBackResult.Should().BeNull();
        }

        [Fact]
        public void SecondBackShouldReturnSecondRecordedItem_WhenItemHasBeenRecorderAfterBackInvoke()
        {
            // given
            _navigatoryHistory.Record(TestNavigationItems.HomePage);
            _navigatoryHistory.Record(TestNavigationItems.Page1);
            _navigatoryHistory.Record(TestNavigationItems.Page2);

            // when
            var firstBackResult = _navigatoryHistory.Back();
            _navigatoryHistory.Record(TestNavigationItems.Page3);
            var secondBackResult = _navigatoryHistory.Back();

            // then
            secondBackResult.Should().Be(TestNavigationItems.Page1);
        }

        [Fact]
        public void FourBacksShouldReturnCorrectItems_WhenRecordAndBackAreInvokedAlternately()
        {
            // given
            _navigatoryHistory.Record(TestNavigationItems.HomePage);
            _navigatoryHistory.Record(TestNavigationItems.Page1);
            _navigatoryHistory.Record(TestNavigationItems.Page2);

            // when
            var firstBackResult = _navigatoryHistory.Back();
            _navigatoryHistory.Record(TestNavigationItems.Page3);
            _navigatoryHistory.Record(TestNavigationItems.Page4);
            var secondBackResult = _navigatoryHistory.Back();
            var thirdBackResult = _navigatoryHistory.Back();
            var fourthBackResult = _navigatoryHistory.Back();
            var fifthBackResult = _navigatoryHistory.Back();

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
            _navigatoryHistory.Record(TestNavigationItems.HomePage);
            _navigatoryHistory.Record(TestNavigationItems.Page1);
            _navigatoryHistory.Record(TestNavigationItems.Page1);

            // then
            var firstBackResult = _navigatoryHistory.Back();
            var secondBackResult = _navigatoryHistory.Back();

            // when
            firstBackResult.Should().Be(TestNavigationItems.HomePage);
            secondBackResult.Should().BeNull();
        }

        [Fact]
        public void CanMoveBackShouldReturnFalse_WhenNoHistoryIsRecorded()
        {
            // given & when & then
            _navigatoryHistory.CanMoveBack().Should().BeFalse();
        }

        [Fact]
        public void BackShouldReturnNull_WhenNoHistoryIsRecorded()
        {
            // given & when & then
            _navigatoryHistory.Back().Should().BeNull();
        }
    }
}
