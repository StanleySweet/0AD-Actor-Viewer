using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public class FileHandler
    {

        public static Actor OpenFile(string filePath)
        {
            Actor _actor;
            // Open the selected file to read.
            using (var athenianDockFile = new StreamReader(@"" + filePath))
            {
                var actorFile = XDocument.Parse(athenianDockFile.ReadToEnd());
                _actor = new Actor(actorFile);
            }
            return _actor;
        }


        public static bool SaveFile(Actor actor)
        {
            using (var file = new StreamWriter(@"G:\test.xml"))
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    ConformanceLevel = ConformanceLevel.Document,
                    OmitXmlDeclaration = false,
                    CloseOutput = true,
                    Indent = true,
                    IndentChars = "  ",
                    NewLineHandling = NewLineHandling.Replace
                };
                using (XmlWriter writer = XmlWriter.Create(file, settings))
                {
                    XDocument document = new XDocument();
                    document.Add(actor.SerializeElements());
                    document.WriteTo(writer);
                    writer.Close();
                }
            }
            return true;
        }
    }
}
