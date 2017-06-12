using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipLoading {

        [Test, Description("remplissage classique")]
        public void ShipLoading1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Station s2 = u.GetStation(2);
            Corporation corp = new Corporation(1);
            Ship ship = s.CreateShip(corp);
            Hangar h = s.CreateHangar(corp);
            ResourceElement e = new ResourceElement(ResourceElement.ResourceType.Wastes, s, 100, 1);
            h.Add(new ResourceStack(e));

            s.CreateHangar(corp);
            ShipDestination dest = ship.AddDestination(s);
            dest.AddLoad(ResourceElement.ResourceType.Wastes, 100);

            ShipDestination dest2 = ship.AddDestination(s2);

            ship.Start();

            for (int ite = 0; ite < 10; ite++) {
                u.Update();
            }

            Assert.AreEqual(100, ship.Cargo.GetResourceQte(ResourceElement.ResourceType.Wastes));

            u.Update();//chargerment
            u.Update();//sortie
            Assert.IsNull(ship.CurrentStation);
        }

        [Test, Description("Dechargement classique")]
        public void ShipUnloading1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Station s2 = u.GetStation(2);
            Corporation corp = new Corporation(1);
            Ship ship = s.CreateShip(corp);
            Hangar h = s.CreateHangar(corp);
            ResourceElement e = new ResourceElement(ResourceElement.ResourceType.Wastes, s, 100, 1);
            ship.Cargo.Add(new ResourceStack(e));

            ShipDestination dest = ship.AddDestination(s);
            dest.AddUnload(ResourceElement.ResourceType.Wastes, 100);

            ShipDestination dest2 = ship.AddDestination(s2);

            ship.Start();

            for (int ite = 0; ite < 10; ite++) {
                u.Update();
            }

            Assert.AreEqual(0, ship.Cargo.GetResourceQte(ResourceElement.ResourceType.Wastes));
            Assert.AreEqual(100, h.GetResourceQte(ResourceElement.ResourceType.Wastes));
        }
    }
}