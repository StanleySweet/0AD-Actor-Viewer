using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Reflection;

namespace ActorEditor.Model.Tests
{
    [TestClass]
    public class VariantTest
    {
        private Variant _variantFile;

        [TestInitialize]
        public void TestInitialize()
        {
            _variantFile = FileHandler.Open0adXmlFile<Variant>(@"E:\ActorEditor\ActorEditor.Model.Tests\test_mod\art\variants\variant_example.xml");
        }

        [TestMethod]
        public void XMLVariantParseFrequency()
        {
            Assert.AreEqual(_variantFile.Frequency, (uint)1);
        }

        [DataTestMethod]
        [DataRow(null, null)]
        [DataRow((uint)3, (uint)3)]
        public void XMLVariantParseFrequency2(uint a, uint b)
        {
            _variantFile.Frequency = a;
            Assert.AreEqual(_variantFile.Frequency, b);
        }

        [TestMethod]
        public void XMLVariantParseMesh()
        {
            Assert.AreEqual(_variantFile.Mesh, "structural/celt_blacksmith_struct1.dae");
        }

        [TestMethod]
        public void XMLParseName()
        {
            Assert.AreEqual(_variantFile.Name, "Blacksmith test");
        }

        [TestMethod]
        public void XMLVariantParseParticle()
        {
            Assert.AreEqual(_variantFile.Particle, null);
        }

        [TestMethod]
        public void XMLVariantParseParentRelativePath()
        {
            Assert.AreEqual(_variantFile.ParentVariantRelativePath, null);
        }
    }
}
