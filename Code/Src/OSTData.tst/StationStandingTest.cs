using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class StationStandingTest {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Basic standing test")]
        public void StandingTest1() {
            Universe universe = new Universe(0);
            Station s = universe.GetStation(1);
            Corporation corp = universe.CreateCorp(1);
            Ship ship = s.CreateShip(corp);
            ShipDestination dest = ship.AddDestination(s);
            dest.AddLoad(ResourceElement.ResourceType.Wastes, 1000);
            ship.Start();

            Hangar corpHangar = s.CreateHangar(corp);

            ResourceElement elem = new ResourceElement(ResourceElement.ResourceType.Wastes, s, 2000, 1);
            ResourceStack stack = new ResourceStack(elem);
            corpHangar.Add(stack);

            Assert.IsTrue(s.Type == Station.StationType.City);
            Assert.IsTrue(System.Math.Abs(s.GetStanding(ResourceElement.ResourceType.Water, 1) - Station.defaultStanding) < 0.0001);
            Assert.IsTrue(System.Math.Abs(s.GetStanding(ResourceElement.ResourceType.Wastes, 1) - Station.defaultStanding) < 0.0001);

            //simuler une augmentation de standing
            for (int i = 0; i < 10; i++) {
                universe.Update();
            }

            //apres 2 jours de chargement...
            // day1 : 0.0 + (1 * 0.05) = 0.05
            // day2 : 0.05 + ((1-0.05) * 0.05) = 0.0975
            Assert.IsTrue(System.Math.Abs(s.GetStanding(ResourceElement.ResourceType.Wastes, 1) - 0.0975) < 0.0001);
        }
    }
}