using NUnit.Framework;

namespace ActorEditor.Model.Tests
{
    [TestFixture]
    public class VariantTest
    {
        private Variant _variantFile;

        [OneTimeSetUp]
        public void TestInitialize()
        {
            _variantFile = FileHandler.Open0adXmlFile<Variant>(@"E:\ActorEditor\ActorEditor.Model.NUnit.Tests\test_mod\art\variants\variant_example.xml");
        }

        [Test]
        public void XMLVariantParseFrequency()
        {
            Assert.AreEqual(_variantFile.Frequency, (uint)1);
        }

        [Test, Sequential]
        public void XMLVariantParseFrequencySequential([Values(null,(uint) 2, (uint)3)] uint a, [Values(null, (uint)2, (uint)3)] uint b)
        {
            _variantFile.Frequency = a;
            Assert.AreEqual(_variantFile.Frequency, b);
        }

        [Test]
        public void XMLVariantParseMesh()
        {
            Assert.AreEqual(_variantFile.Mesh, "structural/celt_blacksmith_struct1.dae");
        }

        [Test]
        public void XMLVariantParseName()
        {
            Assert.AreEqual(_variantFile.Name, "Blacksmith test");
        }

        [Test]
        public void XMLVariantParseParticle()
        {
            Assert.AreEqual(_variantFile.Particle, null);
        }

        [Test]
        public void XMLVariantParseParentRelativePath()
        {
            Assert.AreEqual(_variantFile.ParentVariantRelativePath, null);
        }
    }
}
