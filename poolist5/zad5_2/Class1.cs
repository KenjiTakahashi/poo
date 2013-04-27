using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad5_2 {
    public class Person { }

    public class PersonRegistry {
        /// <summary>
        /// Pierwszy stopień swobody - różne wczytywanie
        /// </summary>
        public List<Person> _persons = new List<Person>();
        /// <summary>
        /// Drugi stopień swobody - różne użycie
        /// </summary>
        public void NotifyPersons() {
            foreach(Person person in _persons)
                Console.WriteLine(person);
        }
    }

    public interface INotification {
        void Notify();
    }

    public class SMSNotification : INotification {
        public void Notify() { }
    }

    public class MailNotification : INotification {
        public void Notify() { }
    }

    class Abstraction {
        public INotification _notification { private get; set; }

        public void Notify() {
            this._notification.Notify();
        }
    }

    class XMLRegistry : Abstraction {
        public IList<Person> Load() {
            return new List<Person>() { new Person() };
        }
    }

    class DBRegistry : Abstraction {
        public IList<Person> Load() {
            return new List<Person>();
        }
    }
}
