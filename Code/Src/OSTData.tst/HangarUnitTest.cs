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
        [Ignore("Issue#12")]
        public void HangarConstruction() {
            Hangar h = new Hangar(station, corporation);

            Assert.AreEqual(h.Station, station);
            Assert.AreEqual(h.Corporation, corporation);
            Assert.AreEqual(h.GetResourceQte(ResourceElement.ResourceType.Wastes), 0);            
        }

        [Test, Description("test de transaction sur un hangar")]
        [Ignore("Issue#12")]
        public void HangarTransactions1() {
            Hangar h = new Hangar(station,corporation);

            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 1);
            ResourceStack stack1 = new ResourceStack(elem1);

            h.Add(stack1);
            Assert.AreEqual(h.GetResourceQte(ResourceElement.ResourceType.Wastes), 100);
            Assert.AreEqual(stack1.Qte, 0);

            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.ToxicWaste, station, 50, 2);
            ResourceStack stack2 = new ResourceStack(elem2);
            h.Add(stack2);
            Assert.AreEqual(h.GetResourceQte(ResourceElement.ResourceType.Wastes), 100);
            Assert.AreEqual(stack1.Qte, 0);
            Assert.AreEqual(h.GetResourceQte(ResourceElement.ResourceType.ToxicWaste), 50);
            Assert.AreEqual(stack2.Qte, 0);
        }
    }
}