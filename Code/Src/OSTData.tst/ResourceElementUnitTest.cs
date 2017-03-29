using NUnit.Framework;

namespace OSTData.tst {
    [TestFixture]
    public class ResourceElementUnitTest {

        private Station station = null;

        [SetUp]
        public void Init() {
            station = new Station();
        }

        [Test, Description("test de la construction")]
        [Ignore("Issue#9")]
        public void ElementConstruction() {
            //test d'un constructeur avec parametres
            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            Assert.AreEqual(elem2.Type, ResourceElement.ResourceType.Water);
            Assert.AreEqual(elem2.Qte, 100);
            Assert.AreEqual(elem2.DateProd, 200);
            Assert.AreEqual(elem2.Station, station);
        }

        [Test, Description("Division d'un ResourceStack en 2, cas normaux")]
        [Ignore("Issue#9")]
        public void ElementSplit() {
            //enlever 25 resource a elem1 pour creer elem2
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            ResourceElement elem2 = elem1.Split(25);
            Assert.AreEqual(elem2.Qte, 25);
            Assert.AreEqual(elem1.Qte, 75);
            Assert.AreEqual(elem2.Type, ResourceElement.ResourceType.Water);
            Assert.AreEqual(elem2.Station, station);
            Assert.AreEqual(elem2.DateProd, 200);
        }

        [Test, Description("Division d'un ResourceStack en 2, cas d'erreur")]
        [Ignore("Issue#9")]
        public void ElementSplitError() {
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);

            //cas si qte trop grande
            ResourceElement elem2 = elem1.Split(200);
            Assert.AreEqual(elem2, null);
            Assert.AreEqual(elem1.Qte, 100);

            //cas si qte egale
            ResourceElement elem3 = elem1.Split(100);
            Assert.AreEqual(elem3, null);
            Assert.AreEqual(elem1.Qte, 100);

            ResourceElement elem4 = elem1.Split(-1);
            Assert.AreEqual(elem4, null);
            Assert.AreEqual(elem1.Qte, 100);
        }
    }
}