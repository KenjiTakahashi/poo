using NUnit.Framework;
using System;
using System.Threading;
using zad1;

namespace zad1tests {
    [TestFixture]
    public class SingleSingletonTest {
        [Test]
        public void Returns_same_instance() {
            SingleSingleton singleton1 = SingleSingleton.Instance;
            SingleSingleton singleton2 = SingleSingleton.Instance;

            Assert.AreSame(singleton1, singleton2);
        }
    }

    [TestFixture]
    public class ThreadSingletonTest {
        [Test]
        public void Returns_same_instance_for_same_thread() {
            ThreadSingleton singleton1 = ThreadSingleton.Instance;
            ThreadSingleton singleton2 = ThreadSingleton.Instance;

            Assert.AreSame(singleton1, singleton2);
        }
        [Test]
        public void Returns_different_instances_for_different_threads() {
            ThreadSingleton singleton1 = ThreadSingleton.Instance;
            ThreadSingleton singleton2 = null;
            var thread = new Thread(() => {
                singleton2 = ThreadSingleton.Instance;
            });
            thread.Start();
            thread.Join();

            Assert.AreNotSame(singleton1, singleton2);
        }
    }

    [TestFixture]
    public class FiveSecondSingletonTest {
        [Test]
        public void Returns_same_instance_for_immediate_calls() {
            FiveSecondSingleton singleton1 = FiveSecondSingleton.Instance;
            FiveSecondSingleton singleton2 = FiveSecondSingleton.Instance;

            Assert.AreSame(singleton1, singleton2);
        }
        [Test]
        public void Returns_same_instance_for_within_five_seconds() {
            FiveSecondSingleton singleton1 = FiveSecondSingleton.Instance;
            Thread.Sleep(4000);
            FiveSecondSingleton singleton2 = FiveSecondSingleton.Instance;

            Assert.AreSame(singleton1, singleton2);
        }
        [Test]
        public void Returns_different_instances_for_more_than_five_seconds() {
            FiveSecondSingleton singleton1 = FiveSecondSingleton.Instance;
            Thread.Sleep(6000);
            FiveSecondSingleton singleton2 = FiveSecondSingleton.Instance;

            Assert.AreNotSame(singleton1, singleton2);
        }
    }
}
