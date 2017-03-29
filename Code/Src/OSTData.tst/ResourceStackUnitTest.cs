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
        public void StackConstruction() {
            //test d'un constructeur avec parametres
            ResourceStack stack = new ResourceStack(ResourceElement.ResourceType.Wastes);

            Assert.AreEqual(stack.Qte, 0);
            Assert.AreEqual(stack.Type, ResourceElement.ResourceType.Wastes);

            ResourceElement elem = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            ResourceStack stack2 = new ResourceStack(elem);
            Assert.AreEqual(elem.Qte, 100);
            Assert.AreEqual(elem.Type, ResourceElement.ResourceType.Water);                
        }

    }
}