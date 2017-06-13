using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipCreationTest {

        [Test, Description("creation de base")]
        public void ShipCreation1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);
            Corporation corp = u.CreateCorp(1);
            Ship ship = s.CreateShip(corp);

            Assert.NotNull(ship);
        }
    }
}