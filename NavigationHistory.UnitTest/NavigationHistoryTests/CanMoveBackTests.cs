﻿using FluentAssertions;
using NavigationHistory.UnitTest.TestData;
using System;
using Xunit;

namespace NavigationHistory.UnitTest.NavigationHistoryTests
{
    public class CanMoveBackTests : IDisposable
    {
        private Lib.NavigationHistory<TestNavigationItem> _navigationHistory;

        public CanMoveBackTests()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        public void Dispose()
        {
            _navigationHistory = new Lib.NavigationHistory<TestNavigationItem>();
        }

        [Fact]
        public void CanMoveBackShouldReturnFalse_WhenNoHistoryIsRecorded()
        {
            // given & when & then
            _navigationHistory.CanMoveBack().Should().BeFalse();
        }
    }
}
