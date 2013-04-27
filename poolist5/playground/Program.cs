using list4zad3;
using netDumbster.smtp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad1;
using zad2;
using zad3;
using zad4;

namespace playground {
    class TimeProviderMock : ITimeProvider {
        public DateTime Now { get; set; }
    }

    class Program {
        class IntSorter : IComparer {
            #region IComparer Members
            public int Compare( object x, object y ) {
                return ( (int)x ).CompareTo( (int)y );
            }
            #endregion
        }

        /* this is the Comparison<int> to be converted */
        static int IntComparer( int x, int y ) {
            return x.CompareTo( y );
        }

        static void Main( string[] args ) {
            List<int> l = new List<int>() { 4, 3, 2, 5, 3, 2, 1 };
            /* 
               convert the IComparer to Comparison<int>
               (x,y) => ( new IntSorter() ).Compare( x, y ) 
               is the answer
            */
            l.Sort((x, y) => (new IntSorter()).Compare(x, y));
            /* test */
            l.ForEach(i => Console.Write(i));
            Console.WriteLine();

            /* second question */
            /* the ArrayList's Sort method accepts ONLY an IComparer */

            /* The "old" way */
            ArrayList a1 = new ArrayList() { 1, 5, 3, 3, 2, 4, 3 };
            a1.Sort(new ComparisonComparer<int>(IntComparer));
            for(int i = 0; i < a1.Count; ++i) {
                Console.Write(a1[i]);
            }
            Console.WriteLine();

            // The .NET >= 4.5 way
            ArrayList a2 = new ArrayList() { 1, 5, 3, 3, 2, 4, 3 };
            a2.Sort(Comparer<int>.Create(IntComparer));
            for(int i = 0; i < a2.Count; ++i) {
                Console.Write(a2[i]);
            }
            Console.ReadLine();
        }
    }
}
