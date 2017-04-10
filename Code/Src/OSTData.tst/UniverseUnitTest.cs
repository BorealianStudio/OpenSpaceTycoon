using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class UniverseTest {

        [SetUp]
        public void Init() {
        }

        [Test, Description("Creation")]
        public void UniverseConstruction() {
            Universe universe = new Universe(0);

            Assert.AreEqual(60, universe.GetStations().Count);
        }

        [Test, Description("univers code en dur")]
        public void UniverseHardCode() {
            Universe universe = new Universe(0);

            Assert.AreEqual(60, universe.GetStations().Count);
            Assert.AreEqual(6, GetNbStationOfType(universe, Station.StationType.Mine));
            Assert.AreEqual(2, GetNbStationOfType(universe, Station.StationType.IceField));
        }

        private int GetNbStationOfType(Universe u, Station.StationType type) {
            int result = 0;
            foreach (Station s in u.GetStations()) {
                if (s.Type == type)
                    result++;
            }
            return result;
        }
    }
}