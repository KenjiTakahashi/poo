using NUnit.Framework;
using System;
using System.Collections;
using zad2;

namespace zad2tests {
    [TestFixture]
    public class GenericFactoryTest {
        [Test]
        public void Returns_instance_of_correct_type() {
            GenericFactory factory = new GenericFactory();
            object obj = factory.CreateObject("System.Collections.ArrayList", false);

            Assert.IsInstanceOf<System.Collections.ArrayList>(obj);
        }

        [Test]
        public void Properly_propagates_parameters() {
            GenericFactory factory = new GenericFactory();
            ArrayList obj = (ArrayList)factory.CreateObject("System.Collections.ArrayList", false, new int[] { 1, 2 });

            Assert.Contains(1, obj);
        }

        [Test]
        public void Returns_different_instances_if_not_singleton() {
            GenericFactory factory = new GenericFactory();
            object obj1 = factory.CreateObject("System.Collections.ArrayList", false, new int[] { 1 });
            object obj2 = factory.CreateObject("System.Collections.ArrayList", false, new int[] { 1 });

            Assert.AreNotSame(obj1, obj2);
        }

        [Test]
        public void Returns_same_instance_if_singleton() {
            GenericFactory factory = new GenericFactory();
            object obj1 = factory.CreateObject("System.Collections.ArrayList", true, new int[] { 1 });
            object obj2 = factory.CreateObject("System.Collections.ArrayList", true, new int[] { 1, 2 });

            Assert.AreSame(obj1, obj2);
        }

        [Test]
        public void String_test() {
            GenericFactory factory = new GenericFactory();
            object obj = factory.CreateObject("System.String", true, 'u', 'w', 'r');

            Assert.IsInstanceOf<System.String>(obj);
            Assert.AreEqual("uwr", obj);
        }
    }
}
