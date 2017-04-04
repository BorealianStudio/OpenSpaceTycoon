using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class ResourceElementUnitTest {
        private Station station = null;

        [SetUp]
        public void Init() {
            station = new Station(Station.StationType.Agricultural, null, new OSTTools.Vector3D());
        }

        [Test, Description("test de la construction")]
        public void ElementConstruction() {
            //test d'un constructeur avec parametres
            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            Assert.AreEqual(ResourceElement.ResourceType.Water, elem2.Type);
            Assert.AreEqual(100, elem2.Quantity);
            Assert.AreEqual(200, elem2.DateProd);
            Assert.AreEqual(station, elem2.Station);
        }

        [Test, Description("Division d'un ResourceStack en 2, cas normaux")]
        public void ElementDivide() {
            //enlever 25 resource a elem1 pour creer elem2
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            ResourceElement elem2 = elem1.Split(25);
            Assert.AreEqual(25, elem2.Quantity);
            Assert.AreEqual(75, elem1.Quantity);
            Assert.AreEqual(ResourceElement.ResourceType.Water, elem2.Type);
            Assert.AreEqual(station, elem2.Station);
            Assert.AreEqual(200, elem2.DateProd);
        }

        [Test, Description("Division d'un ResourceStack en 2, cas d'erreur")]
        public void ElementDivideError() {
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);

            //cas si qte trop grande
            ResourceElement elem2 = elem1.Split(200);
            Assert.AreEqual(null, elem2);
            Assert.AreEqual(100, elem1.Quantity);

            //cas si qte impossible
            ResourceElement elem4 = elem1.Split(-1);
            Assert.AreEqual(null, elem4);
            Assert.AreEqual(100, elem1.Quantity);

            //cas si qte egale
            ResourceElement elem3 = elem1.Split(100);
            Assert.AreEqual(100, elem3.Quantity); // elem3 est une copie de elem1
            Assert.AreEqual(0, elem1.Quantity);
        }
    }
}