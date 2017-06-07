using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class GameProductionTests {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Creation")]
        public void GameProduction1() {
            Game g = new Game();

            Station city1 = g.Universe.GetStation(1);
            Assert.NotNull(city1);
            Assert.AreEqual(Station.StationType.City, city1.Type);

            Hangar h1 = city1.GetHangar(-1);
            Assert.NotNull(h1);

            //100 update = 20 jours
            for (int i = 0; i < 100; i++) {
                g.Update();
            }

            //100 waste par jour, = 2000 en 20 jours. Les gens present recoivent les ressources
            Assert.AreEqual(0, city1.GetHangar(-1).GetResourceQte(ResourceElement.ResourceType.Wastes));
            ///todo add a load for player

            return;
        }
    }
}