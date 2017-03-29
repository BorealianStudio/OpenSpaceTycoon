using NUnit.Framework;

namespace OSTData.tst {
    [TestFixture]
    public class ResourceStackUnitTest {

        private Station station = null;

        [SetUp]
        public void Init() {
            station = new Station();
        }

        [Test, Description("test de la construction")]
        [Ignore("Issue#11")]
        public void Construction() {
            //test d'un constructeur avec parametres
            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            Assert.AreEqual(elem2.Type, ResourceElement.ResourceType.Water);
            Assert.AreEqual(elem2.Qte, 100);
            Assert.AreEqual(elem2.DateProd, 200);
            Assert.AreEqual(elem2.Station, station);
        }

    }
}