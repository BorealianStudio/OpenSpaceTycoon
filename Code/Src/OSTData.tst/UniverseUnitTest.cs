using NUnit.Framework;

namespace OSTData.tst {
    [TestFixture]
    public class UniverseTest {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Creation")]
        public void Construction1() {
            Universe universe = new Universe(0);

            Assert.AreEqual(universe.GetStations().Count, 60);
        }
    }
}