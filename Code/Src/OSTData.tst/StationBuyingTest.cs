using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class StationBuyingTest {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Prix d'achat initial")]
        public void StandingTest1() {
            Universe universe = new Universe(0);
            Station s = universe.GetStation(1);

            Assert.IsTrue(System.Math.Abs(s.GetBuyingPrice(ResourceElement.ResourceType.Food) - 150.0f) < 0.0001);
        }
    }
}