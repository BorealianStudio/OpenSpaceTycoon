using System.Collections.Generic;

using NUnit.Framework;

namespace OSTData.tst {
    [TestFixture]
    public class HangarUnitTest {

        private Station station = null;
        private Corporation corporation = null;

        [SetUp]
        public void Init() {
            station = new Station(Station.StationType.Agricultural,null, new OSTTools.Vector3D());
        }

        [Test, Description("test de la construction")]
        public void HangarConstruction() {
            Hangar h = new Hangar(station, corporation);

            Assert.AreEqual(station, h.Station);
            Assert.AreEqual(corporation, h.Corporation);
            Assert.AreEqual(0, h.GetResourceQte(ResourceElement.ResourceType.Wastes));
        }

        [Test, Description("test de transaction sur un hangar")]
        public void HangarTransactions1() {
            Hangar h = new Hangar(station,corporation);

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
    }
}