using System;
using System.Collections.Generic;

namespace NavigationHistory.Lib
{
    public class NavigationHistory<TItem> where TItem : class, INavigationHistoryItem
    {
        private readonly IList<TItem> _backHistory;

        private readonly IList<TItem> _forwardHistory;

        public NavigationHistory() : this(10)
        {
        }

        public NavigationHistory(int maxHistorySize)
        {
            MaxHistorySize = maxHistorySize;
            _backHistory = new List<TItem>();
            _forwardHistory = new List<TItem>();
        }

        public int MaxHistorySize { get; }

        public TItem CurrentItem { get; private set; }

        public void Record(TItem historyItemToRecord)
        {
            Record(historyItemToRecord, true);
        }

        public TItem Forward()
        {
            if (!CanMoveForward())
            {
                return null;
            }

            var lastForwardHistoryItem = GetLastItemAndRemoteIt(_forwardHistory);
            Record(lastForwardHistoryItem, false);
            return lastForwardHistoryItem;
        }

        public bool CanMoveForward()
        {
            return _forwardHistory.Count > 0;
        }

        public TItem Back()
        {
            AddToForwardHistory(CurrentItem);

            if (!CanMoveBack())
            {
                return null;
            }

            CurrentItem = GetLastItemAndRemoteIt(_backHistory);
            return CurrentItem;
        }

        public bool CanMoveBack()
        {
            return _backHistory.Count > 0;
        }

        private TItem GetLastItemAndRemoteIt(IList<TItem> history)
        {
            if (history.Count < 1)
            {
                return null;
            }

            var lastItem = history[history.Count - 1];
            history.Remove(lastItem);
            return lastItem;
        }

        private void AddToForwardHistory(TItem historyItem)
        {
            if (historyItem == null)
            {
                return;
            }

            _forwardHistory.Add(historyItem);
            RemoveOldHistoryItem(_forwardHistory);
        }

        private void AddtoBackHistory(TItem historyItem, bool cleanForwardHistory = true)
        {
            _backHistory.Add(historyItem);

            if (cleanForwardHistory)
            {
                _forwardHistory.Clear();
            }
        }

        private void RemoveOldHistoryItem(IList<TItem> _forwardHistory)
        {
            if (_forwardHistory.Count > MaxHistorySize)
            {
                _forwardHistory.RemoveAt(0);
            }
        }

        private void Record(TItem historyItemToRecord, bool cleanForwardHistory)
        {
            if (historyItemToRecord == null)
            {
                throw new ArgumentNullException(nameof(historyItemToRecord));
            }

            if (CurrentItem?.Identifier == historyItemToRecord.Identifier)
            {
                return;
            }

            if (CurrentItem != null)
            {
                AddtoBackHistory(CurrentItem, cleanForwardHistory);
                RemoveOldHistoryItem(_backHistory);
            }

            CurrentItem = historyItemToRecord;
        }
    }
}