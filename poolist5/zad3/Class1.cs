using list4zad3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zad3 {
    public interface ITimeProvider {
        DateTime Now { get; }
    }

    public class TimeProvider : ITimeProvider {
        public DateTime Now {
            get { return DateTime.Now; }
        }
    }

    public class AirportProxy : IAirport {
        private ITimeProvider _timeProvider;
        private Airport _airport;

        public AirportProxy(int n) {
            this._timeProvider = new TimeProvider();
            this._airport = new Airport(n);
        }

        public AirportProxy(int n, ITimeProvider timeProvider) {
            this._timeProvider = timeProvider;
            this._airport = new Airport(n);
        }

        public Plane AcquirePlane() {
            Console.WriteLine(this._timeProvider.Now);
            if(this._timeProvider.Now.Hour < 8 || this._timeProvider.Now.Hour > 21)
                throw new UnauthorizedAccessException();
            return this._airport.AcquirePlane();
        }

        public void ReleasePlane(Plane plane) {
            this._airport.ReleasePlane(plane);
        }
    }
}
