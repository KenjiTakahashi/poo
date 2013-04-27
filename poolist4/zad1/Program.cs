using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    public class SingleSingleton {
        //private static readonly SingleSingleton _instance = new Singleton();
        private static SingleSingleton _instance;
        private static readonly object _padlock = new Object();

        SingleSingleton() {}

        public static SingleSingleton Instance {
            get {
                if(_instance == null) {
                    lock(_padlock) {
                        if(_instance == null) {
                            _instance = new SingleSingleton();
                        }
                    }
                }
                return _instance;
            }
        }
    }

    public class ThreadSingleton {
        [ThreadStatic]
        private static ThreadSingleton _instance;

        ThreadSingleton() {}

        public static ThreadSingleton Instance {
            get {
                if(_instance == null) {
                    _instance = new ThreadSingleton();
                }
                return _instance;
            }
        }
    }

    public class FiveSecondSingleton {
        private static FiveSecondSingleton _instance;
        private static readonly object _padlock = new Object();
        private static DateTime _expirationTime;

        FiveSecondSingleton() {}

        public static FiveSecondSingleton Instance {
            get {
                DateTime now = DateTime.Now;
                if(_expirationTime < now) {
                    _instance = new FiveSecondSingleton();
                    _expirationTime = now.AddSeconds(5);
                }
                return _instance;
            }
        }
    }
}