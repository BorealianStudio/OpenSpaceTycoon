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

        [Test, Description("test date")]
        public void UniverseDate() {
            Universe u = new Universe(0);
            for (int i = 0; i < 10; i++) {
                u.Update();
            }
            Assert.AreEqual(2, u.Day);
            Assert.AreEqual(0, u.Hour);
            u.Update();
            Assert.AreEqual(2, u.Day);
            Assert.AreEqual(1, u.Hour);
        }

        [Test, Description("test de la cohérence des constructions")]
        public void UniverseData1() {
            Universe u = new Universe(0);
            Portal p1 = u.Portals[0];
            Assert.AreNotEqual(0, p1.Station1.Gates.Count);
            Assert.AreNotEqual(0, p1.Station2.Gates.Count);
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