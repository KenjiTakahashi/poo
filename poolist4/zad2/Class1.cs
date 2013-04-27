using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad2
{
    public class GenericFactory {
        private static Dictionary<string, object> _singletons = new Dictionary<string, object>();

        public object CreateObject(string TypeName, bool IsSingleton, params object[] Parameters) {
            if(IsSingleton) {
                if(!_singletons.ContainsKey(TypeName)) {
                    _singletons[TypeName] = Activator.CreateInstance(Type.GetType(TypeName), Parameters);
                }
                return _singletons[TypeName];
            }
            return Activator.CreateInstance(Type.GetType(TypeName), Parameters);
        }
    }
}
