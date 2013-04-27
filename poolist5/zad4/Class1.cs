using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad4 {
    // in .Net >= 4.5 we can just use Comparer<T>.Create(Comparison<T>)
    public class ComparisonComparer<T> : IComparer<T>, IComparer {
        private readonly Comparison<T> _comparison;

        public ComparisonComparer(Comparison<T> comparison) {
            this._comparison = comparison;
        }

        #region IComparer<T> Members

        public int Compare(T x, T y) {
            return this._comparison(x, y);
        }

        #endregion

        #region IComparer Members

        public int Compare(object x, object y) {
            return this._comparison((T)x, (T)y);
        }

        #endregion
    }
}
