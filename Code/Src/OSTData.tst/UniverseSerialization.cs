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
            Universe universe = new Universe(1);

            string s = JsonConvert.SerializeObject(universe, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ObjectCreationHandling = ObjectCreationHandling.Replace
            });

            System.IO.File.WriteAllText("c:\\sandbox\\Debug.txt", s);

            Newtonsoft.Json.Serialization.MemoryTraceWriter traceWriter = new Newtonsoft.Json.Serialization.MemoryTraceWriter();
            traceWriter.LevelFilter = System.Diagnostics.TraceLevel.Info;

            Universe universe2 = JsonConvert.DeserializeObject<Universe>(s, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TraceWriter = traceWriter
            });
            Assert.IsTrue(universe.Equals(universe2));

            //            System.IO.File.WriteAllText("c:\\sandbox\\output.txt", traceWriter.ToString());
        }
    }
}