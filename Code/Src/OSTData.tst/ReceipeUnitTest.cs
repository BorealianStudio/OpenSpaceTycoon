using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ReceipeUnitTest {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Creation")]
        public void UniverseConstruction() {
            Receipe r = new Receipe(10);

            Assert.AreEqual(10, r.MaxFreq);
        }
    }
}