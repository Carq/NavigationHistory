using FluentAssertions;
using NavigationHistory.UnitTest.TestData;
using System;
using Xunit;

namespace NavigationHistory.UnitTest.NavigationHistoryTests
{
    public class RecordTests : IDisposable
    {
        private Lib.NavigationHistory<TestNavigationItem> _navigationHistory;

        public RecordTests()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        public void Dispose()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        [Fact]
        public void ShouldNotRecordDuplicatedItems()
        {
            // given
            _navigationHistory.Record(new TestNavigationItem("1"));
            _navigationHistory.Record(new TestNavigationItem("1"));
            _navigationHistory.Record(new TestNavigationItem("1"));

            // when & then
            _navigationHistory.CanMoveBack().Should().BeFalse();
        }
    }
}
