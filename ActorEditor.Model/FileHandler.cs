using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public class FileHandler
    {

        public static Actor OpenActorFile(string filePath)
        {
            Actor actor;
            // Open the selected file to read.

            try
            {
                using (var actorStream = new StreamReader(@"" + filePath))
                {
                    var actorFile = XDocument.Parse(actorStream.ReadToEnd());
                    actor = new Actor(actorFile);
                }

            }
            catch (Exception ex)
            {
                actor = null;
                Debug.WriteLine("Error: Parsed file is either malformed or not a actor file.");
            }

            return actor;
        }

        public static Variant OpenVariantFile(string filePath)
        {
            Variant variant;
            // Open the selected file to read.
            try
            {
                using (var athenianDockFile = new StreamReader(@"" + filePath))
                {
                    var variantFile = XDocument.Parse(athenianDockFile.ReadToEnd());
                    variant = new Variant(variantFile);
                }

            }catch(Exception ex)
            {
                variant = null;
                Debug.WriteLine("Error: Parsed file is either malformed or not a variant file.");
            }

            return variant;
        }

        /// <summary>
        /// Returns the list of materials
        /// </summary>
        /// <returns></returns>
        public static List<string> GetMaterialList()
        {
            string path = @"E:\materials\";
            var pathList = Directory.GetFiles(path).ToList();
            for (int i = 0; i != pathList.Count;++i)
            {
                pathList[i] = pathList[i].Replace(path, "");
            }
            return pathList;
        }

        /// <summary>
        /// Saves the actor file
        /// </summary>
        /// <param name="actor"></param>
        /// <returns></returns>
        public static bool SaveFile(Actor actor)
        {
            using (var file = new StreamWriter(@"E:\test.xml"))
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
        /// <summary>
        /// Saves the file
        /// </summary>
        /// <param name="variant"></param>
        /// <returns></returns>
        public static bool SaveFile(Variant variant)
        {
            using (var file = new StreamWriter(@"E:\test.xml"))
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
                    document.Add(variant.SerializeElements());
                    document.WriteTo(writer);
                    writer.Close();
                }
            }
            return true;
        }
    }
}
