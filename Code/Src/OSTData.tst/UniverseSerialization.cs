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
            Newtonsoft.Json.Serialization.MemoryTraceWriter traceWriter = new Newtonsoft.Json.Serialization.MemoryTraceWriter();
            traceWriter.LevelFilter = System.Diagnostics.TraceLevel.Info;

            JsonSerializerSettings settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TraceWriter = traceWriter
            };

            Universe universe = new Universe(1);
            string s = JsonConvert.SerializeObject(universe, settings);
            Universe universe2 = JsonConvert.DeserializeObject<Universe>(s, settings);

            Assert.IsTrue(universe.Equals(universe2));

            System.IO.File.WriteAllText("d:\\sandbox\\Debug.txt", s);
            //System.IO.File.WriteAllText("c:\\sandbox\\output.txt", traceWriter.ToString());
        }

        [Test, Description("Test a savoir si le seed est conservé")]
        public void UniverseSerializationRandomSeed() {
            Newtonsoft.Json.Serialization.MemoryTraceWriter traceWriter = new Newtonsoft.Json.Serialization.MemoryTraceWriter();
            traceWriter.LevelFilter = System.Diagnostics.TraceLevel.Info;

            JsonSerializerSettings settings = new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                TraceWriter = traceWriter
            };

            Universe universe = new Universe(1);
            string s = JsonConvert.SerializeObject(universe, settings);
            Universe universe2 = JsonConvert.DeserializeObject<Universe>(s, settings);
            int a = universe.Random.Next();
            int b = universe2.Random.Next();
            Assert.AreEqual(universe.Random.Next(), universe2.Random.Next());
        }
    }
}