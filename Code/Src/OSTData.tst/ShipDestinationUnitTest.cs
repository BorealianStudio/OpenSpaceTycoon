using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipDestinationUnitTest {
        private Universe u = null;
        private Corporation corp = null;

        [SetUp]
        public void init() {
            u = new Universe(0);
            corp = u.CreateCorp(1);
            corp.AddICU(300, "");
        }

        [Test, Description("construction")]
        public void ShipDestinationCreation() {
            Station s = u.GetStation(1);
            Ship ship = s.CreateShip(corp);

            ShipDestination dest = ship.AddDestination(s);

            Assert.NotNull(dest);
            Assert.AreEqual(s.ID, dest.Destination.ID);

            dest.AddLoad(ResourceElement.ResourceType.Wastes, 100);

            dest.AddUnload(ResourceElement.ResourceType.Water, 200);

            Assert.AreEqual(1, dest.GetLoads().Count);
            Assert.AreEqual(ResourceElement.ResourceType.Wastes, dest.GetLoads()[0].type);
            Assert.AreEqual(100, dest.GetLoads()[0].qte);

            Assert.AreEqual(1, dest.GetUnloads().Count);
            Assert.AreEqual(ResourceElement.ResourceType.Water, dest.GetUnloads()[0].type);
            Assert.AreEqual(200, dest.GetUnloads()[0].qte);
        }

        [Test, Description("Suppression simple")]
        public void ShipDestinationRemoveSimple() {
            Station s = u.GetStation(1);
            Ship ship = s.CreateShip(corp);

            bool eventDone = false;
            ship.onDestinationChange += () => { eventDone = true; };

            ShipDestination dest1 = ship.AddDestination(u.GetStation(10));
            ShipDestination dest2 = ship.AddDestination(u.GetStation(20));

            Assert.IsTrue(eventDone);
            eventDone = false;

            ship.RemoveDestination(dest1);
            Assert.IsTrue(eventDone);

            Assert.AreEqual(1, ship.GetDestinations().Count);
            Assert.AreEqual(dest2, ship.GetDestinations()[0]);
        }

        [Test, Description("suppression quand en marche")]
        public void ShipDestinationRemovePriorToCurrent() {
            Station s = u.GetStation(1);
            Ship ship = s.CreateShip(corp);

            ShipDestination d1 = ship.AddDestination(u.GetStation(10));
            ShipDestination d2 = ship.AddDestination(s);
            ship.Start();

            for (int i = 0; i < 12; i++) {
                u.Update();
            }

            Assert.AreEqual(d2, ship.CurrentDestination);
            ship.RemoveDestination(d1);

            Assert.AreEqual(d2, ship.CurrentDestination);
        }

        [Test, Description("suppression destination en cours")]
        public void ShipDestinationRemoveUsed() {
            Station s = u.GetStation(1);
            Ship ship = s.CreateShip(corp);

            ShipDestination dest1 = ship.AddDestination(u.GetStation(10));
            ShipDestination dest2 = ship.AddDestination(u.GetStation(15));
            ship.Start();

            ship.RemoveDestination(dest1);

            Assert.AreEqual(2, ship.GetDestinations().Count);
        }

        [Test, Description("Suppression destination autres ship")]
        public void ShipDestinationRemoveWrong() {
            Station s = u.GetStation(1);
            Ship s1 = s.CreateShip(corp);
            Ship s2 = s.CreateShip(corp);

            ShipDestination dest1 = s1.AddDestination(u.GetStation(10));
            ShipDestination dest2 = s1.AddDestination(u.GetStation(20));

            ShipDestination dest3 = s2.AddDestination(u.GetStation(10));
            ShipDestination dest4 = s2.AddDestination(u.GetStation(20));
            s2.RemoveDestination(dest1);

            Assert.AreEqual(2, s2.GetDestinations().Count);
        }

        [Test, Description("Suppression d'une destination presente 2 fois")]
        public void ShipDestinationRemoveDouble() {
            Station s = u.GetStation(1);
            Ship ship = s.CreateShip(corp);

            ShipDestination dest1 = ship.AddDestination(u.GetStation(10));
            ShipDestination dest2 = ship.AddDestination(u.GetStation(20));
            ShipDestination dest3 = ship.AddDestination(u.GetStation(10));
            ShipDestination dest4 = ship.AddDestination(u.GetStation(20));
            ship.RemoveDestination(dest1);

            Assert.AreEqual(3, ship.GetDestinations().Count);
        }
    }
}