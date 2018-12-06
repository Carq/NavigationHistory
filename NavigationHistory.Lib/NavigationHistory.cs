using System;
using System.Collections.Generic;

namespace NavigationHistory.Lib
{
    public class NavigationHistory
    {
        private readonly IList<INavigationHistoryItem> _backHistory;

        private INavigationHistoryItem _currentItem;

        public NavigationHistory() : this(10)
        {
        }

        public NavigationHistory(int maxHistorySize)
        {
            MaxHistorySize = maxHistorySize;
            _backHistory = new List<INavigationHistoryItem>();
        }

        public int MaxHistorySize { get; }

        public void Record(INavigationHistoryItem historyItemToRecord)
        {
            if (historyItemToRecord == null)
            {
                throw new ArgumentNullException(nameof(historyItemToRecord));
            }

            if (_currentItem == historyItemToRecord)
            {
                return;
            }

            if (_currentItem != null)
            {
                _backHistory.Add(_currentItem);
                RemoveOldHistoryItem();
            }

            _currentItem = historyItemToRecord;
        }

        public INavigationHistoryItem Back()
        {
            if (!CanMoveBack())
            {
                return null;
            }

            _currentItem = GetLastItemAndRemoteIt();
            return _currentItem;
        }

        public bool CanMoveBack()
        {
            return _backHistory.Count > 0;
        }

        private INavigationHistoryItem GetLastItemAndRemoteIt()
        {
            if (_backHistory.Count < 1)
            {
                return null;
            }

            var lastItem = _backHistory[_backHistory.Count - 1];
            _backHistory.Remove(lastItem);
            return lastItem;
        }

        private void RemoveOldHistoryItem()
        {
            if (_backHistory.Count > MaxHistorySize)
            {
                _backHistory.RemoveAt(0);
            }
        }
    }
}