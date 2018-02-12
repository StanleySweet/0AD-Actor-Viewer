using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Linq;

namespace ActorEditor.Model.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private Actor _athenianDockActor;

        [TestInitialize]
        public void TestInitialize()
        {
            using (var athenianDockFile = new StreamReader(@"G:\ActorEditor\ActorEditor.Model.Tests\athenian_dock.xml"))
            {
                var actorFile = XDocument.Parse(athenianDockFile.ReadToEnd());
                _athenianDockActor = new Actor(actorFile);
                
            }
        } 

        [TestMethod]
        public void XMLParse()
        {
            Assert.AreEqual(_athenianDockActor.Version, (uint) 1);
            Assert.AreEqual(true, _athenianDockActor.Floats);
            Assert.AreEqual(true, _athenianDockActor.CastsShadows);
            Assert.AreEqual("player_trans_ao_parallax_spec.xml", _athenianDockActor.Material);
        }
    }
}
