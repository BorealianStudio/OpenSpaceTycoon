using NUnit.Framework;

using Newtonsoft.Json;

namespace OSTData.tst {

    [TestFixture]
    public class UniverseSerialization {

        [SetUp]
        public void Init() {
        }

        [Test, Description("savegame1")]
        public void UniverseSave1() {
            Universe universe = new Universe(0);

            string s = JsonConvert.SerializeObject(universe, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Serialize, Formatting = Formatting.Indented, PreserveReferencesHandling = PreserveReferencesHandling.All });

            //            System.IO.File.WriteAllText("D:\\sandbox\\Debug.txt", s);

            Universe universe2 = JsonConvert.DeserializeObject<Universe>(s);

            //            Assert.IsTrue(universe.Equals(universe2));
        }
    }
}