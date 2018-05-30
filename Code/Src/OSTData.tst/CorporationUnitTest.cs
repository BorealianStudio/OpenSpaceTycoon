using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class CorporationUnitTest {

        [Test, Description("Gestion des ICU")]
        public void CorporationICUs1() {
            Universe u = new Universe(0);
            Corporation corp = u.CreateCorp(1);

            Assert.AreEqual(100, corp.ICU);

            bool eventTest = false;
            corp.onICUChange += (i) => { eventTest = true; };

            corp.AddICU(1000, "");
            Assert.AreEqual(1100, corp.ICU);
            Assert.IsTrue(eventTest);
            eventTest = false;

            corp.RemoveICU(750, "");
            Assert.AreEqual(350, corp.ICU);
            Assert.IsTrue(eventTest);
            eventTest = false;

            corp.RemoveICU(750, "");
            Assert.AreEqual(-400, corp.ICU);
            Assert.IsTrue(eventTest);
            eventTest = false;
        }
        
    }


}