using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipUnitTest {
        private Station s1 = null;
        private Station s2 = null;

        [SetUp]
        public void Init() {
            Universe u = new Universe(0);
            foreach (Station s in u.GetStations()) {
                if (null == s1) {
                    s1 = s;
                    continue;
                }
                if (null == s2) {
                    s2 = s;
                    continue;
                }
                break;
            }
        }

        [Test, Description("construction")]
        public void ShipConstruction() {
        }

        [Test, Description("destination ajout")]
        public void ShipDestinations() {
            Ship ship = s1.CreateShip();
            ship.AddDestination(s1);

            ship.AddDestination(s2, 0);
            Assert.AreEqual(s2, ship.Destinations[0]);
            Assert.AreEqual(s1, ship.Destinations[1]);
            Assert.AreEqual(2, ship.Destinations.Count);
        }

        [Test, Description("gestion destinations quand en route")]
        public void ShipDestinationsRunning() {
        }
    }
}