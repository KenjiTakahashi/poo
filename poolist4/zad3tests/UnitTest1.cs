using NUnit.Framework;
using System;
using zad3;

namespace zad3tests {
    [TestFixture]
    public class AirportTest {
        [Test]
        public void Returns_instance_of_correct_type() {
            Airport airport = new Airport(10);
            object plane = airport.AcquirePlane();

            Assert.IsInstanceOf<Plane>(plane);
        }

        [Test]
        [ExpectedException(typeof(NoPlanesException))]
        public void Raises_exception_when_no_more_planes_available() {
            Airport airport = new Airport(1);
            Plane plane1 = airport.AcquirePlane();
            Plane plane2 = airport.AcquirePlane();
        }

        [Test]
        public void Properly_releases_planes() {
            Airport airport = new Airport(1);
            Plane plane1 = airport.AcquirePlane();
            airport.ReleasePlane(plane1);

            Assert.DoesNotThrow(delegate { Plane plane1again = airport.AcquirePlane(); });
        }

        [Test]
        [ExpectedException(typeof(InvalidPlaneException))]
        public void Raises_exception_when_invalid_plane_is_released() {
            Airport airport = new Airport(1);
            Plane planeInvalid = new Plane();
            airport.ReleasePlane(planeInvalid);
        }
    }
}