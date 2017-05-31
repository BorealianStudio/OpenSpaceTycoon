using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipLoading {

        [Test, Description("remplissage classique")]
        public void ShipLoading1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Corporation corp = new Corporation(1);
            Ship ship = s.CreateShip(corp);
            Hangar h = s.CreateHangar(corp);
            ResourceElement e = new ResourceElement(ResourceElement.ResourceType.Wastes, s, 100, 1);
            h.Add(new ResourceStack(e));

            ShipDestination dest = ship.AddDestination(s);
            dest.AddLoad(ResourceElement.ResourceType.Wastes, 100);

            ship.Start();

            for (int ite = 0; ite < 10; ite++) {
                u.Update();
            }

            Assert.AreEqual(50, ship.Cargo.GetResourceQte(ResourceElement.ResourceType.Wastes));
        }
    }
}