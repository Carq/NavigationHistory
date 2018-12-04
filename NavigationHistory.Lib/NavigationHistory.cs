using System;
using System.Collections.Generic;

namespace NavigationHistory.Lib
{
    public class NavigationHistory
    {
        private readonly Stack<INavigationHistoryItem> _history;

        private INavigationHistoryItem _currentItem;

        public NavigationHistory()
        {
            MaxHistorySize = 10;
            _history = new Stack<INavigationHistoryItem>();
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

            _history.Push(_currentItem);
            _currentItem = historyItemToRecord;
        }

        public INavigationHistoryItem Back()
        {
            if (!CanMoveBack())
            {
                return null;
            }

            _currentItem = _history.Pop();
            return _currentItem;
        }

        public bool CanMoveBack()
        {
            return _history.Count > 0;
        }
    }
}
