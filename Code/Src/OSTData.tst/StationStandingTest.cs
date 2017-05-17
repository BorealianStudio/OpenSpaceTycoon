using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class StationStandingTest {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Creation")]
        public void StandingTest1() {
            Universe universe = new Universe(0);
            Station s = universe.GetStation(1);
            Ship ship = s.CreateShip();

            Assert.IsTrue(s.Type == Station.StationType.City);
            Assert.IsTrue(s.GetStanding(ResourceElement.ResourceType.Water, 1) < 0.0);
            Assert.IsTrue(s.GetStanding(ResourceElement.ResourceType.Wastes, 1) < 0.0);

            //simuler une augmentation de standing
            for (int i = 0; i < 10; i++) {
                universe.Update();
            }

            //todo le test
        }
    }
}