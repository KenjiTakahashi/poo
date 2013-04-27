using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using zad4;

namespace zad4tests {
    [TestFixture]
    public class ComparisonComparerTest {
        [Test]
        public void Should_convert_Comparison_to_Comparer() {
            Comparison<int> comparison = new Comparison<int>((x, y) => x.CompareTo(y));
            ComparisonComparer<int> adapter = new ComparisonComparer<int>(comparison);

            Assert.IsInstanceOf<IComparer<int>>(adapter);
            Assert.IsInstanceOf<IComparer>(adapter);
        }

        [Test]
        public void Should_properly_sort_an_array() {
            //ArrayList accepts only Comparer in Sort() method.
            Comparison<int> comparison = new Comparison<int>((x, y) => x.CompareTo(y));
            ComparisonComparer<int> adapter = new ComparisonComparer<int>(comparison);
            ArrayList a1 = new ArrayList() { 1, 5, 3, 3, 2, 4, 3 };
            a1.Sort(adapter);

            Assert.AreEqual(new ArrayList() { 1, 2, 3, 3, 3, 4, 5 }, a1);
        }
    }
}
