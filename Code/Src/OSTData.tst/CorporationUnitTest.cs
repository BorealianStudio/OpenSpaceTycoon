using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class CorporationUnitTest {

        [Test, Description("gestion des ICUs")]
        public void CorporationICUs1() {
            Universe u = new Universe(0);
            Corporation corp = u.CreateCorp(1);

            Assert.AreEqual(0, corp.ICU);

            bool eventTest = false;
            corp.onICUChange += (i) => { eventTest = true; };

            corp.AddICU(1000, "");
            Assert.AreEqual(1000, corp.ICU);
            Assert.IsTrue(eventTest);
            eventTest = false;

            corp.RemoveICU(750, "");
            Assert.AreEqual(250, corp.ICU);
            Assert.IsTrue(eventTest);
            eventTest = false;

            corp.RemoveICU(750, "");
            Assert.AreEqual(-500, corp.ICU);
            Assert.IsTrue(eventTest);
            eventTest = false;
        }
    }
}