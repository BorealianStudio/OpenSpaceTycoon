using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipMovingtest {

        [Test, Description("un deplacement sans chargement")]
        public void ShipMoving1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Station s2 = u.GetStation(2);
            Corporation corp = u.CreateCorp(1);
            Ship ship = s.CreateShip(corp);

            ShipDestination dest = ship.AddDestination(s2);
            ShipDestination dest2 = ship.AddDestination(s);

            ship.Start();
            Assert.NotNull(ship.CurrentStation);
            Assert.AreEqual(s.ID, ship.CurrentStation.ID);

            u.Update();//on devrait sortir
            Assert.IsNull(ship.CurrentStation);

            for (int ite = 0; ite < 9; ite++) { //station suivante
                u.Update();
            }
            Assert.NotNull(ship.CurrentStation);
            Assert.AreEqual(s2.ID, ship.CurrentStation.ID);

            for (int ite = 0; ite < 11; ite++) { //sortir + station suivante
                u.Update();
            }
            Assert.NotNull(ship.CurrentStation);
            Assert.AreEqual(s.ID, ship.CurrentStation.ID);
        }
    }
}