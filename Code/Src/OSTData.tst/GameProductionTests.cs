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
            for (int i = 0; i < 100; i++) {
                g.Update();
            }

            Station city1 = g.Universe.GetStation(1);
            Assert.NotNull(city1);

            Hangar h1 = city1.GetHangar(-1);
            Assert.NotNull(h1);
        }
    }
}