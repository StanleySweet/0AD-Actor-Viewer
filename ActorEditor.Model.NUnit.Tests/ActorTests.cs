using NUnit.Framework;

namespace ActorEditor.Model.Tests
{
    [TestFixture]
    public class ActorTest
    {
        private Actor _athenianDockActor;

        [OneTimeSetUp]
        public void Init()
        {
            _athenianDockActor = FileHandler.Open0adXmlFile<Actor>(@"E:\ActorEditor\ActorEditor.Model.NUnit.Tests\test_mod\art\actors\structures\civname\dock.xml");
        } 

        [Test]
        public void XMLActorParse()
        {
            Assert.AreEqual(_athenianDockActor.Version, (uint) 1);
            Assert.AreEqual(true, _athenianDockActor.Floats);
            Assert.AreEqual(true, _athenianDockActor.CastsShadows);
            Assert.AreEqual("player_trans_ao_parallax_spec.xml", _athenianDockActor.Material);
        }
    }
}
