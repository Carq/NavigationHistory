using FluentAssertions;
using NavigationHistory.UnitTest.TestData;
using System;
using Xunit;

namespace NavigationHistory.UnitTest.NavigationHistoryTests
{
    public class BackTests : IDisposable
    {
        private Lib.NavigationHistory<TestNavigationItem> _navigationHistory;

        public BackTests()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        public void Dispose()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        [Fact]
        public void ShouldReturnFirstInsertedItem_WhenTwoRecordedItemsAreInHistory()
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
        public void BackShouldReturnNull_WhenTwoRecordedItemsAreInHistory()
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
        public void BackShouldReturnNull_WhenNoHistoryIsRecorded()
        {
            // given & when & then
            _navigationHistory.Back().Should().BeNull();
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
    }
}