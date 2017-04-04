using System.Collections.Generic;

using NUnit.Framework;

namespace OSTData.tst {
    [TestFixture]
    public class ResourceStackUnitTest {

        private Station station = null;

        [SetUp]
        public void Init() {
            station = new Station(Station.StationType.Agricultural, null, new OSTTools.Vector3D());
        }

        [Test, Description("test de la construction")]
        public void StackConstruction() {

            //test d'un constructeur avec parametres
            ResourceStack stack = new ResourceStack(ResourceElement.ResourceType.Wastes);

            Assert.AreEqual(0, stack.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Wastes, stack.Type);

            ResourceElement elem = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            ResourceStack stack2 = new ResourceStack(elem);
            Assert.AreEqual(100, stack2.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Water, stack2.Type);
            Assert.AreEqual(0, elem.Quantity);
        }

        [Test, Description("test d'ajout d'elements")]
        public void StackAdd() {
            ResourceStack stack = new ResourceStack(ResourceElement.ResourceType.Wastes);
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 200);
            stack.Add(elem1);
            Assert.AreEqual(100, stack.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Wastes, stack.Type);
            Assert.AreEqual(0, elem1.Quantity);

            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 50, 201);
            stack.Add(elem2);

            Assert.AreEqual(100, stack.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Wastes, stack.Type);
            Assert.AreEqual(50, elem2.Quantity);

            ResourceElement elem3 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 200, 300);
            stack.Add(elem3);
            Assert.AreEqual(300, stack.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Wastes, stack.Type);
            Assert.AreEqual(0, elem3.Quantity);
        }

        [Test, Description("Ajout d'un stack a un stack")]
        public void StackAddStack() {
            ResourceStack stack1 = new ResourceStack(ResourceElement.ResourceType.Water);
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 1);
            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 50, 2);
            stack1.Add(elem1);
            stack1.Add(elem2);

            ResourceStack stack2 = new ResourceStack(ResourceElement.ResourceType.Water);
            ResourceElement elem3 = new ResourceElement(ResourceElement.ResourceType.Water, station, 25, 3);
            stack2.Add(elem3);

            stack1.Add(stack2);

            Assert.AreEqual(175, stack1.Qte);
            Assert.AreEqual(0, stack2.Qte);

            ResourceElement elem4 = new ResourceElement(ResourceElement.ResourceType.ToxicWaste, station, 1, 1);
            ResourceStack stack3 = new ResourceStack(elem4);
            stack1.Add(stack3);
            Assert.AreEqual(175, stack1.Qte);
            Assert.AreEqual(1, stack3.Qte);                
        }

        [Test, Description("creation d'un substack")]
        public void StackSubStack() {
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 1);
            ResourceStack stack = new ResourceStack(elem1);
            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 2);
            stack.Add(elem2);

            ResourceStack s1 = stack.GetSubStack(25);
            Assert.AreEqual(25, s1.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Water, s1.Type);
            Assert.AreEqual(175, stack.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Water, stack.Type);
            foreach (ResourceElement e in s1.GetElements()) {
                Assert.AreEqual(1, e.DateProd);
            }

            ResourceStack s2 = stack.GetSubStack(200);
            Assert.AreEqual(null, s2);
            Assert.AreEqual(175, stack.Qte);

            //retirer un stack composé de 2 elements
            ResourceStack s3 = stack.GetSubStack(100);
            Assert.AreEqual(100, s3.Qte);
            Assert.AreEqual(ResourceElement.ResourceType.Water, s3.Type);
            Assert.AreEqual(75, stack.Qte);
            List<ResourceElement> elemsStack = stack.GetElements();
            Assert.AreEqual(1, elemsStack.Count);
            Assert.AreEqual(2, elemsStack[0].DateProd);
            List<ResourceElement> elems = s3.GetElements();
            Assert.AreEqual(2, elems.Count);
            Assert.AreEqual(1, elems[0].DateProd);
            Assert.AreEqual(75, elems[0].Quantity);
            Assert.AreEqual(2, elems[1].DateProd);
            Assert.AreEqual(25, elems[1].Quantity);
        }
    }
}