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
        [Ignore("Issue#8")]
        public void Construction() {
            ResourceElement elem = new ResourceElement();

            Assert.AreEqual(elem.Type,ResourceElement.ResourceType.Unknown);

            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);

            Assert.AreEqual(elem2.Type, ResourceElement.ResourceType.Water);
            Assert.AreEqual(elem2.Qte, 100);
            Assert.AreEqual(elem2.DateProd,200);
            Assert.AreEqual(elem2.Station, station);
        }

        [Test, Description("Division d'un ResourceStack en 2")]
        [Ignore("Issue#8")]
        public void Divide() {
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water,station,100,200);
            ResourceElement elem2 = elem1.Divide(25);
            
            Assert.AreEqual(elem2.Qte, 25);
            Assert.AreEqual(elem1.Qte, 75);
            Assert.AreEqual(elem2.Type,ResourceElement.ResourceType.Water);
            Assert.AreEqual(elem2.Station, station);
            Assert.AreEqual(elem2.DateProd, 200);
        }
    }
}