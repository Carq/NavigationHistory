using System;
using System.Collections.Generic;

namespace NavigationHistory.Lib
{
    public class NavigationHistory
    {
        private readonly IList<INavigationHistoryItem> _backHistory;

        private readonly IList<INavigationHistoryItem> _forwardHistory;

        private INavigationHistoryItem _currentItem;

        public NavigationHistory() : this(10)
        {
        }

        public NavigationHistory(int maxHistorySize)
        {
            MaxHistorySize = maxHistorySize;
            _backHistory = new List<INavigationHistoryItem>();
            _forwardHistory = new List<INavigationHistoryItem>();
        }

        public int MaxHistorySize { get; }

        public void Record(INavigationHistoryItem historyItemToRecord)
        {
            Record(historyItemToRecord, true);
        }

        public INavigationHistoryItem Forward()
        {
            if (!CanMoveForward())
            {
                return null;
            }

            var lastForwardHistoryItem = GetLastItemAndRemoteIt(_forwardHistory);
            Record(lastForwardHistoryItem, false);
            return lastForwardHistoryItem;
        }

        private bool CanMoveForward()
        {
            return _forwardHistory.Count > 0;
        }

        public INavigationHistoryItem Back()
        {
            AddToForwardHistory(_currentItem);

            if (!CanMoveBack())
            {
                return null;
            }

            _currentItem = GetLastItemAndRemoteIt(_backHistory);
            return _currentItem;
        }

        public bool CanMoveBack()
        {
            return _backHistory.Count > 0;
        }

        private INavigationHistoryItem GetLastItemAndRemoteIt(IList<INavigationHistoryItem> history)
        {
            if (history.Count < 1)
            {
                return null;
            }

            var lastItem = history[history.Count - 1];
            history.Remove(lastItem);
            return lastItem;
        }

        private void AddToForwardHistory(INavigationHistoryItem historyItem)
        {
            if (historyItem == null)
            {
                return;
            }

            _forwardHistory.Add(historyItem);
            RemoveOldHistoryItem(_forwardHistory);
        }

        private void AddtoBackHistory(INavigationHistoryItem historyItem, bool cleanForwardHistory = true)
        {
            _backHistory.Add(historyItem);

            if (cleanForwardHistory)
            {
                _forwardHistory.Clear();
            }
        }

        private void RemoveOldHistoryItem(IList<INavigationHistoryItem> _forwardHistory)
        {
            if (_forwardHistory.Count > MaxHistorySize)
            {
                _forwardHistory.RemoveAt(0);
            }
        }

        private void Record(INavigationHistoryItem historyItemToRecord, bool cleanForwardHistory)
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
                AddtoBackHistory(_currentItem, cleanForwardHistory);
                RemoveOldHistoryItem(_backHistory);
            }

            _currentItem = historyItemToRecord;
        }
    }
}