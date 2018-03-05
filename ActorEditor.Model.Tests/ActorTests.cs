using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Xml.Linq;

namespace ActorEditor.Model.Tests
{
    [TestClass]
    public class ActorTest
    {
        private Actor _athenianDockActor;

        [TestInitialize]
        public void TestInitialize()
        {
            _athenianDockActor = FileHandler.Open0adXmlFile<Actor>(@"E:\ActorEditor\ActorEditor.Model.Tests\test_mod\art\actors\structures\civname\dock.xml");
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
