using System.Collections.Generic;

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
        public void StackConstruction() {

            //test d'un constructeur avec parametres
            ResourceStack stack = new ResourceStack(ResourceElement.ResourceType.Wastes);

            Assert.AreEqual(stack.Qte, 0);
            Assert.AreEqual(stack.Type, ResourceElement.ResourceType.Wastes);

            ResourceElement elem = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 200);
            ResourceStack stack2 = new ResourceStack(elem);
            Assert.AreEqual(stack2.Qte, 100);
            Assert.AreEqual(stack2.Type, ResourceElement.ResourceType.Water);
            Assert.AreEqual(elem.Quantity, 0);
        }

        [Test, Description("test d'ajout d'elements")]
        public void StackAdd() {
            ResourceStack stack = new ResourceStack(ResourceElement.ResourceType.Wastes);
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 100, 200);
            stack.Add(elem1);
            Assert.AreEqual(stack.Qte, 100);
            Assert.AreEqual(stack.Type, ResourceElement.ResourceType.Wastes);
            Assert.AreEqual(elem1.Quantity, 0);

            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 50, 201);
            stack.Add(elem2);

            Assert.AreEqual(stack.Qte, 100);
            Assert.AreEqual(stack.Type, ResourceElement.ResourceType.Wastes);
            Assert.AreEqual(elem2.Quantity, 50);

            ResourceElement elem3 = new ResourceElement(ResourceElement.ResourceType.Wastes, station, 200, 300);
            stack.Add(elem3);
            Assert.AreEqual(stack.Qte, 300);
            Assert.AreEqual(stack.Type, ResourceElement.ResourceType.Wastes);
            Assert.AreEqual(elem3.Quantity, 0);
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
            Assert.AreEqual(stack1.Qte, 175);
            Assert.AreEqual(stack2.Qte, 0);

            ResourceElement elem4 = new ResourceElement(ResourceElement.ResourceType.ToxicWaste, station, 1, 1);
            ResourceStack stack3 = new ResourceStack(elem4);
            stack1.Add(stack3);
            Assert.AreEqual(stack1.Qte, 175);
            Assert.AreEqual(stack3.Qte, 1);                
        }

        [Test, Description("creation d'un substack")]
        public void StackSubStack() {
            ResourceElement elem1 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 1);
            ResourceStack stack = new ResourceStack(elem1);
            ResourceElement elem2 = new ResourceElement(ResourceElement.ResourceType.Water, station, 100, 2);

            ResourceStack s1 = stack.GetSubStack(25);
            Assert.AreEqual(s1.Qte, 25);
            Assert.AreEqual(s1.Type, ResourceElement.ResourceType.Water);
            Assert.AreEqual(stack.Qte, 175);
            Assert.AreEqual(stack.Type, ResourceElement.ResourceType.Water);
            foreach (ResourceElement e in s1.GetElements()) {
                Assert.AreEqual(e.DateProd, 1);
            }

            ResourceStack s2 = stack.GetSubStack(200);
            Assert.AreEqual(s2, null);
            Assert.AreEqual(stack.Qte, 175);

            //retirer un stack composé de 2 elements
            ResourceStack s3 = stack.GetSubStack(100);
            Assert.AreEqual(s3.Qte, 100);
            Assert.AreEqual(s3.Type, ResourceElement.ResourceType.Water);
            Assert.AreEqual(stack.Qte, 75);
            List<ResourceElement> elemsStack = stack.GetElements();
            Assert.AreEqual(elemsStack.Count, 1);
            Assert.AreEqual(elemsStack[0].DateProd, 2);
            List<ResourceElement> elems = s3.GetElements();
            Assert.AreEqual(elems.Count, 2);
            Assert.AreEqual(elems[0].DateProd, 1);
            Assert.AreEqual(elems[0].Quantity, 75);
            Assert.AreEqual(elems[1].DateProd, 2);
            Assert.AreEqual(elems[1].Quantity, 25);
        }
    }
}