using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad1 {
    [Serializable()]
    public class InterfaceNotRegisteredException : System.Exception {
        public InterfaceNotRegisteredException() : base() { }

        protected InterfaceNotRegisteredException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

    public interface IDIContainer {
        void RegisterType<T>(bool singleton) where T : class;

        void RegisterType<From, To>() where To : From;

        T Resolve<T>();
    }

    public class DIContainer : IDIContainer {
        private Dictionary<Type, object> _singletons;
        private Dictionary<Type, Type> _registers;

        public DIContainer() {
            this._singletons = new Dictionary<Type, object>();
            this._registers = new Dictionary<Type, Type>();
        }

        public void RegisterType<T>(bool singleton) where T : class {
            if(!this._registers.ContainsKey(typeof(T)))
                throw new InterfaceNotRegisteredException();
            if(singleton)
                this._singletons[typeof(T)] = null;
            else
                this._singletons.Remove(typeof(T));
        }

        public void RegisterType<From, To>() where To : From {
            this._registers[typeof(From)] = typeof(To);
        }

        public T Resolve<T>() {
            if(!this._registers.ContainsKey(typeof(T)))
                throw new InterfaceNotRegisteredException();
            if(this._singletons.ContainsKey(typeof(T))) {
                if(this._singletons[typeof(T)] == null)
                    this._singletons[typeof(T)] = Activator.CreateInstance(this._registers[typeof(T)]);
                return (T)this._singletons[typeof(T)];
            }
            return (T)Activator.CreateInstance(this._registers[typeof(T)]);
        }
    }
}
