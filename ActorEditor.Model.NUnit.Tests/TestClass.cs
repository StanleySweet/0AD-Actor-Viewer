using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ActorEditor.Model.NUnit.Tests
{
    [TestFixture]
    public class TestParser
    {
        [Test]
        public void TestIncorrectFiles()
        {
            var _athenianDockActor = FileHandler.Open0adXmlFile<Actor>(@"E:\ActorEditor\ActorEditor.Model.Tests\test_mod\art\actors\structures\civname\dock.xml");
            Assert.AreEqual(FileHandler.Open0adXmlFile<Variant>(@"E:\ActorEditor\ActorEditor.Model.Tests\test_mod\art\actors\structures\civname\dock.xml"), null);
            var xDocument = XDocument.Parse(@"<?xml version=""1.0"" encoding=""utf - 8""?><root version=""1"" ></root>");
            Assert.Throws(typeof(TypeLoadException), () => { new Actor().DeserializeElements(xDocument.Root); });
            Assert.Throws(typeof(TypeLoadException), () => { new Variant().DeserializeElements(xDocument.Root); });
        }
    }
}
