using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ShipLoading {

        [Test, Description("remplissage classique")]
        public void ShipLoading1() {
            Universe u = new Universe(0);
            Ship ship = new Ship(1);

            for (int ite = 0; ite < 10; ite++) {
                u.Update();
            }
        }
    }
}