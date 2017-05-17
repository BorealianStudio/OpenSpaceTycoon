using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipLoading {

        [Test, Description("remplissage classique")]
        public void ShipLoading1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Ship ship = s.CreateShip();

            ShipDestination dest = ship.AddDestination(s);
            dest.AddLoad(ResourceElement.ResourceType.Wastes, 100);

            ship.Start();

            for (int ite = 0; ite < 10; ite++) {
                u.Update();
            }

            Assert.AreEqual(100, ship.Cargo.GetResourceQte(ResourceElement.ResourceType.Wastes));
        }
    }
}