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
            using (var athenianDockFile = new StreamReader(@"C:\Dev\ActorEditor\ActorEditor.Model.Tests\athenian_dock.xml"))
            {
                var actorFile = XDocument.Parse(athenianDockFile.ReadToEnd());
                _athenianDockActor = new Actor(actorFile);
                
            }
        } 

        [TestMethod]
        public void XMLParse()
        {
            Assert.AreEqual(_athenianDockActor.Version, (uint) 1);
            Assert.AreEqual(_athenianDockActor.Floats, true);
            Assert.AreEqual(_athenianDockActor.CastsShadows, true);
            Assert.AreEqual(_athenianDockActor.Material.FileName, "player_trans_ao_parallax_spec.xml");
        }
    }
}
