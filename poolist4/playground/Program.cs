using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zad2;
using zad4;

namespace playground {
    class Program {
        static void Main(string[] args) {
            //object[] o1 = new object[] { 1 };
            //object[] o2 = new object[] { 1 };
            //Tuple<string, object[]> t1 = new Tuple<string, object[]>("a", new object[] { new int[] { 1 } });
            //Tuple<string, object[]> t2 = new Tuple<string, object[]>("a", new object[] { new int[] { 1 } });
            //Console.WriteLine(o1 == o2);
            //Console.WriteLine(t1 == t2);
            //GenericFactory factory = new GenericFactory();
            //object o = factory.CreateObject("System.String", false, 'u');
            TagBuilder tag = new TagBuilder();
            tag.Indentation = 2;
            tag.prettyPrint = true;
            var script =
                tag.StartTag("parent")
                    .AddAttribute("parentproperty1", "true")
                    .AddAttribute("parentproperty2", "5")
                        .StartTag("child1")
                        .AddAttribute("childproperty1", "c")
                        .AddContent("childbody")
                        .EndTag()
                        .StartTag("child2")
                        .AddAttribute("childproperty2", "c")
                        .AddContent("childbody")
                        .EndTag()
                    .EndTag()
                    .StartTag("script")
                    .AddContent("$.scriptbody();")
                    .EndTag()
                    .ToString();
            Console.WriteLine(script);
            Console.ReadKey();
        }
    }
}
