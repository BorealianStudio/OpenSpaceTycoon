using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipUnitTest {
        private Station s1 = null;
        private Station s2 = null;
        private Station s3 = null;

        [SetUp]
        public void Init() {
            Universe u = new Universe(0);
            s1 = u.GetStation(1);
            s2 = u.GetStation(2);
            s3 = u.GetStation(3);
        }

        [Test, Description("construction")]
        public void ShipConstruction() {
        }

        [Test, Description("destination ajout")]
        public void ShipDestinations() {
            Universe u = new Universe(0);
            Corporation corp = u.CreateCorp(1);
            Ship ship = s1.CreateShip(corp);
            ship.AddDestination(s1);
            ship.AddDestination(s2);
            ship.AddDestination(s3, 1);

            Assert.AreEqual(s1.ID, ship.GetDestination(0).Destination.ID);
            Assert.AreEqual(s3.ID, ship.GetDestination(1).Destination.ID);
            Assert.AreEqual(s2.ID, ship.GetDestination(2).Destination.ID);
        }

        [Test, Description("gestion destinations quand en route")]
        public void ShipDestinationsRunning() {
        }
    }
}