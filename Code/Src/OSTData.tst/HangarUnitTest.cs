using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class HangarUnitTest {
        private Station station = null;
        private Corporation corporation = null;

        [SetUp]
        public void Init() {
            Universe universe = new Universe(0);
            StarSystem system = new StarSystem(1, universe, new OSTTools.Vector3());
            station = new Station(Station.StationType.Agricultural, system, new OSTTools.Vector3(), 1);
        }

        [Test, Description("test de la construction")]
        public void HangarConstruction() {
            Hangar h = new Hangar(station, corporation);

            Assert.AreEqual(station, h.Station);
            Assert.AreEqual(corporation, h.Corporation);
            Assert.AreEqual(0, h.GetResourceQte(ResourceElement.ResourceType.Wastes));
        }

        [Test, Description("test de la suppression d'un stack quand vide")]
        public void HangarStackRemove() {
            Hangar h = new Hangar(station, corporation);

            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 1);
            ResourceStack stack1 = new ResourceStack(elem1);

            bool eventTrigered = false;
            h.Add(stack1);
            h.onRemoveStack += (s) => { eventTrigered = true; };
            h.GetStack(ResourceElement.ResourceType.Wastes, 100);

            Assert.IsTrue(eventTrigered);
            Assert.AreEqual(0, h.ResourceStacks.Count);
        }

        [Test, Description("test de transaction sur un hangar")]
        public void HangarTransactions1() {
            Hangar h = new Hangar(station, corporation);

            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 1);
            ResourceStack stack1 = new ResourceStack(elem1);

            h.Add(stack1);
            Assert.AreEqual(100, h.GetResourceQte(ResourceElement.ResourceType.Wastes));
            Assert.AreEqual(0, stack1.Qte);

            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.ToxicWaste, station, 50, 2);
            ResourceStack stack2 = new ResourceStack(elem2);
            h.Add(stack2);
            Assert.AreEqual(100, h.GetResourceQte(ResourceElement.ResourceType.Wastes));
            Assert.AreEqual(0, stack1.Qte);
            Assert.AreEqual(50, h.GetResourceQte(ResourceElement.ResourceType.ToxicWaste));
            Assert.AreEqual(0, stack2.Qte);
        }

        [Test, Description("test pour retirer des ressources")]
        public void HangarRemoveResources() {
            Hangar h = new Hangar(station, corporation);

            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 1);
            ResourceStack stack1 = new ResourceStack(elem1);
            h.Add(stack1);

            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 2);
            ResourceElement elem3 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 3);
            ResourceStack stack2 = new ResourceStack(elem2);
            stack2.Add(elem3);
            h.Add(stack2);

            ResourceStack outStack = h.GetStack(ResourceElement.ResourceType.Wastes, 50);
            Assert.NotNull(outStack);
            Assert.AreEqual(50, outStack.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Wastes, outStack.Type);
            Assert.AreEqual(250, h.GetResourceQte(ResourceElement.ResourceType.Wastes));
        }
    }
}