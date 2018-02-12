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
            XDocument document = new XDocument();
            XElement root = new XElement("actor");
            root.Add(new XAttribute("version", actor.Version));

            if (actor.CastsShadows)
                root.Add(new XElement("castshadows"));
            if (actor.Floats)
                root.Add(new XElement("float"));


            foreach (var group in actor.Groups)
            {
                var curGroup = new XElement("group");
                foreach (var variant in group)
                {
                    var curVariant = new XElement("variant");
                    if (!string.IsNullOrEmpty(variant.Name))
                        curVariant.Add(new XAttribute("name", variant.Name));
                    if (!string.IsNullOrEmpty(variant.Frequency.ToString()))
                        curVariant.Add(new XAttribute("frequency", variant.Frequency));
                    if (!string.IsNullOrEmpty(variant.Mesh))
                        curVariant.Add(new XElement("mesh", variant.Mesh));
                    if (!string.IsNullOrEmpty(variant.Color.ToString()))
                        curVariant.Add(new XElement("color", variant.Color.ToString()));

                    if (variant.Props != null)
                    {
                        var props = new XElement("props");
                        foreach(var prop in variant.Props)
                        {
                            var Xprop = new XElement("prop");
                            Xprop.Add(new XAttribute("attachpoint", prop.AttachPoint));
                            Xprop.Add(new XAttribute("actor", prop.GetRelativePath()));
                            props.Add(Xprop);
                        }


                        curVariant.Add(props);
                    }




                    curGroup.Add(curVariant);
                }
                root.Add(curGroup);
            }
            document.Add(root);

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


            using (var file = new StreamWriter(@"D:\test.xml"))
            {
                using (XmlWriter writer = XmlWriter.Create(file, settings))
                {
                    document.WriteTo(writer);
                    writer.Close();
                }
            }

            return true;
        }

    }
}
