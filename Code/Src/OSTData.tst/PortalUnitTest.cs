using NUnit.Framework;

namespace OSTData.tst {

    [TestFixture]
    public class PortalUnitTest {

        [Test, Description("Positions")]
        public void PortalPositions() {
            Universe u = new Universe(0);

            Portal p = u.Portals[0];
            OSTTools.Vector3 pos = p.Position(p.Station1);
            OSTTools.Vector3 pos2 = p.Position(p.Station2);
        }
    }
}