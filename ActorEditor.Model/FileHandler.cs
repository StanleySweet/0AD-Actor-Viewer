using ActorEditor.Model.Entities.Mod;
using ActorEditor.Model.External;
using ActorEditor.Model.Interfaces;
using Newtonsoft.Json;
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
    /// <summary>
    /// Handles all the io with 0ad files
    /// </summary>
    public class FileHandler
    {
        /// <summary>
        /// Open any 0ad file supported that implements I0adXmlSerializableElement
        /// </summary>
        /// <typeparam name="T">Type of file</typeparam>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static T Open0adXmlFile<T>(string filePath) where T : I0adXmlSerializableElement, new()
        {
            T variant;
            // Open the selected file to read.
            try
            {
                using (var file = new StreamReader(@"" + filePath))
                {
                    variant = new T();
                    variant.DeserializeElements(XDocument.Parse(file.ReadToEnd()).Root);
                }
            }
            catch (Exception ex)
            {
                variant = default(T);
                Debug.WriteLine("Error: Parsed file is either malformed or not a 0AD file. " + ex);
            }

            return variant;
        }

        /// <summary>
        /// Opens a Json file and stores into an ModJson Item
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static ModJsonFile OpenModJsonFile(string filePath)
        {
            ModJsonFile modJsonFile;
            // Open the selected file to read.
            try
            {
                using (var file = new StreamReader(@"" + filePath))
                {

                    modJsonFile = JsonConvert.DeserializeObject<ModJsonFile>(file.ReadToEnd());
                }

            }
            catch (Exception ex)
            {
                modJsonFile = null;
                Debug.WriteLine("Error: Parsed file is either malformed or not a variant file. " + ex);
            }

            return modJsonFile;
        }

        /// <summary>
        /// Returns the list of materials
        /// </summary>
        /// <returns></returns>
        public static string[] GetMaterialList(string path)
        {
            string[] pathList = null;
            try
            {
                pathList = Directory.GetFiles(path);
                for (int i = 0; i != pathList.Length; ++i)
                    pathList[i] = pathList[i].Replace(path + @"\", "");
            }
            catch (Exception)
            {

                Debug.WriteLine("Can't find the material Folder");
            }

            return pathList;
        }

        /// <summary>
        /// Saves the actor file
        /// </summary>
        /// <param name="modJsonFile"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveFile(ModJsonFile modJsonFile, string path)
        {
            File.WriteAllText(@"" + path, JsonConvert.SerializeObject(modJsonFile,
                Newtonsoft.Json.Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }
            ));
            return true;
        }


        /// <summary>
        /// Saves the actor file
        /// </summary>
        /// <param name="actor"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveFile(Actor actor, string path)
        {
            using (var file = new StreamWriter(path))
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Encoding = UpperCaseUTF8Encoding.UpperCaseUTF8,
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
                using (StreamWriter writer = new StreamWriter(path, true))
                {
                    writer.Write("\r\n");
                }
            }
            return true;
        }
        /// <summary>
        /// Saves the file
        /// </summary>
        /// <param name="variant"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static bool SaveFile(Variant variant, string path)
        {
            using (var file = new StreamWriter(path))
            {
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Encoding = UpperCaseUTF8Encoding.UpperCaseUTF8,
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
