using NUnit.Framework;

namespace OSTData.tst {
    [TestFixture]
    public class UniverseTest {

        [SetUp]
        public void Init() {
        }

        [Test]
        public void Construction1() {
        }

        [TestCase(12, 3, Result = 15)]
        public int Test(int a, int b) {
            return a+b;
        }
    }
}