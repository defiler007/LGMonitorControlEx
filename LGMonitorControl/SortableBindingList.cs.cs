using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LGMonitorControl
{
    public class SortableBindingList<T> : BindingList<T>
    {
        private bool _isSorted;
        private ListSortDirection _sortDirection = ListSortDirection.Ascending;
        private PropertyDescriptor _sortProperty;

        public SortableBindingList() { }

        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => _isSorted;
        protected override ListSortDirection SortDirectionCore => _sortDirection;
        protected override PropertyDescriptor SortPropertyCore => _sortProperty;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            _sortProperty = prop;
            _sortDirection = direction;

            var list = Items as List<T>;
            if (list == null) return;

            list.Sort(delegate (T lhs, T rhs)
            {
                var lhsValue = prop.GetValue(lhs);
                var rhsValue = prop.GetValue(rhs);

                int result;
                if (lhsValue == null && rhsValue == null)
                    result = 0;
                else if (lhsValue == null)
                    result = -1;
                else if (rhsValue == null)
                    result = 1;
                else if (lhsValue is IComparable)
                    result = ((IComparable)lhsValue).CompareTo(rhsValue);
                else if (lhsValue.Equals(rhsValue))
                    result = 0;
                else
                    result = lhsValue.ToString().CompareTo(rhsValue.ToString());

                return direction == ListSortDirection.Ascending ? result : -result;
            });

            _isSorted = true;
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            _isSorted = false;
            _sortProperty = null;
        }
    }
}
