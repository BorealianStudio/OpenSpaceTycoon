using NUnit.Framework;

namespace OSTData.tst {
    [TestFixture]
    public class UniverseTest {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Creation")]
        [Ignore("Issue#13")]
        public void Construction1() {
            Universe universe = new Universe(0);

            Assert.AreEqual(60,universe.GetStations().Count);
        }
    }
}