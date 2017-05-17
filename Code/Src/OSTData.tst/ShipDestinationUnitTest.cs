using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipDestinationUnitTest {

        [Test, Description("construction")]
        public void ShipDestinationCreation() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Ship ship = s.CreateShip();

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
    }
}