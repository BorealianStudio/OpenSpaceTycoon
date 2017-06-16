using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipLoading {

        [Test, Description("remplissage classique")]
        public void ShipLoading1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Station s2 = u.GetStation(2);
            Corporation corp = u.CreateCorp(1);
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
            Corporation corp = u.CreateCorp(1);
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

        [Test, Description("dechargement avec vente")]
        public void ShipUnloadingSelling() {
            Universe u = new Universe(0);
            Station city = u.GetStation(1);
            Station city2 = u.GetStation(2);
            Corporation player = u.CreateCorp(1);
            Ship ship = city.CreateShip(player);
            ResourceElement e = new ResourceElement(ResourceElement.ResourceType.Food, city2, 100, 1);
            ResourceStack stack = new ResourceStack(e);
            ship.Cargo.Add(stack);

            Hangar h = city.CreateHangar(player);

            ShipDestination dest = ship.AddDestination(city);
            dest.AddUnload(ResourceElement.ResourceType.Food, 100);
            ship.Start();

            //ca prend 10 fois pour decharger
            for (int i = 0; i < 11; i++) {
                u.Update();
            }

            Assert.AreEqual(0, ship.Cargo.GetResourceQte(ResourceElement.ResourceType.Food));
            Assert.AreEqual(15000, ship.Owner.ICU);
            Assert.AreEqual(100, city.GetHangar(-1).GetResourceQte(ResourceElement.ResourceType.Food));
        }
    }
}