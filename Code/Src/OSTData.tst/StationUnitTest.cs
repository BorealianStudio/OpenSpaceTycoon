using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class StationUnitTest {

        [Test]
        public void StationConstruction() {
        }

        [Test, Description("construction d'une station")]
        public void StationConstruction1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);

            Assert.NotNull(s.GetHangar(-1));
        }

        [Test, Description("test des prix de ressources")]
        public void StationPrices1() {
            Universe u = new Universe(0);
            Station s = u.GetStation(1);

            s.SetBuyingPrice(ResourceElement.ResourceType.Wastes, 200);
            Assert.AreEqual(200, s.GetBuyingPrice(ResourceElement.ResourceType.Wastes));
            Assert.AreEqual(0, s.GetBuyingPrice(ResourceElement.ResourceType.Unknown));
        }
    }
}