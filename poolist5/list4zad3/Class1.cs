using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace list4zad3 {
    [Serializable()]
    public class NoPlanesException : System.Exception {
        public NoPlanesException() : base() { }

        protected NoPlanesException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

    [Serializable()]
    public class InvalidPlaneException : System.Exception {
        public InvalidPlaneException() : base() { }

        protected InvalidPlaneException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

    public class Plane { }

    public interface IAirport {
        Plane AcquirePlane();
        void ReleasePlane(Plane plane);
    }

    public class Airport : IAirport {
        private Stack<Plane> freePlanes;
        private List<Plane> usedPlanes;

        public Airport(int n) {
            freePlanes = new Stack<Plane>(n);
            usedPlanes = new List<Plane>(n);
            for(int i = 0; i < n; ++i) {
                freePlanes.Push(new Plane());
            }
        }

        public Plane AcquirePlane() {
            if(freePlanes.Count <= 0) {
                throw new NoPlanesException();
            }
            Plane plane = freePlanes.Pop();
            usedPlanes.Add(plane);
            return plane;
        }

        public void ReleasePlane(Plane plane) {
            if(!usedPlanes.Contains(plane)) {
                throw new InvalidPlaneException();
            }
            usedPlanes.Remove(plane);
            freePlanes.Push(plane);
        }
    }
}