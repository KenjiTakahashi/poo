using list4zad3;
using NUnit.Framework;
using System;
using zad3;

namespace zad3tests {
    class TimeProviderMock : ITimeProvider {
        public DateTime Now { get; set; }
    }

    [TestFixture]
    public class AirportProxyTest {
        [Test]
        public void Should_acquire_plane_between_8_and_22() {
            TimeProviderMock tp = new TimeProviderMock();
            IAirport airport = new AirportProxy(30, tp);
            for(int i = 9; i < 22; ++i) {
                tp.Now = new DateTime(2012, 01, 01, i, 0, 0);
                Assert.DoesNotThrow(delegate { Plane plane = airport.AcquirePlane(); });
            }

        }

        [Test]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Should_not_acquire_plane_before_8() {
            TimeProviderMock tp = new TimeProviderMock();
            tp.Now = new DateTime(2012, 01, 01, 7, 0, 0);
            IAirport airport = new AirportProxy(30, tp);
            airport.AcquirePlane();
        }

        [Test]
        [ExpectedException(typeof(UnauthorizedAccessException))]
        public void Should_not_acquire_plane_after_22() {
            TimeProviderMock tp = new TimeProviderMock();
            tp.Now = new DateTime(2012, 01, 01, 22, 0, 0);
            IAirport airport = new AirportProxy(30, tp);
            airport.AcquirePlane();
        }
    }
}
