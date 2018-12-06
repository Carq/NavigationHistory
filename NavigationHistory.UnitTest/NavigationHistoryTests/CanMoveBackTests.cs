using FluentAssertions;
using System;
using Xunit;

namespace NavigationHistory.UnitTest.NavigationHistoryTests
{
    public class CanMoveBackTests : IDisposable
    {
        private Lib.NavigationHistory _navigationHistory;

        public CanMoveBackTests()
        {
            _navigationHistory = new Lib.NavigationHistory();
        }

        public void Dispose()
        {
            _navigationHistory = new Lib.NavigationHistory();
        }

        [Fact]
        public void CanMoveBackShouldReturnFalse_WhenNoHistoryIsRecorded()
        {
            // given & when & then
            _navigationHistory.CanMoveBack().Should().BeFalse();
        }
    }
}
