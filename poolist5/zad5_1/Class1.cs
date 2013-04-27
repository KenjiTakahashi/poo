using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad5_1 {
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

    public interface ILoadRegistry {
        IList<Person> Load();
    }

    public class XMLRegistry : ILoadRegistry {
        IList<Person> Load() {
            return new List<Person>() { new Person() };
        }
    }

    public class DBRegistry : ILoadRegistry {
        IList<Person> Load() {
            return new List<Person>();
        }
    }

    class Abstraction {
        public ILoadRegistry _registry { private get; set; }

        public IList<Person> GetPersons() {
            return this._registry.Load();
        }
    }

    class SMSNotification : Abstraction {
        public void Notify() { }
    }

    class MailNotification : Abstraction {
        public void Notify() { }
    }
}
