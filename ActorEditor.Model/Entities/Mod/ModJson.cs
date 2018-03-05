using System;
using System.Collections.Generic;
using System.Text;

namespace ActorEditor.Model.Entities.Mod
{
    public class ModJsonFile
    {
        private string _name;
        private string _versions;
        private string _label;
        private string _url;
        private string _description;
        private string[] _dependencies;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ModJsonFile()
        {

        }

        public ModJsonFile(string file)
        {
        }

        /// <summary>
        /// Internal name of the mod, should not contain special characters
        /// Example : mod_test
        /// </summary>
        public string Name { get => _name; set => _name = value; }
        /// <summary>
        /// Version of the mod for compatibility checks
        /// Example: 0.0.23
        /// </summary>
        public string Versions { get => _versions; set => _versions = value; }
        /// <summary>
        /// Real name of the mod
        /// Example: Test Mod
        /// </summary>
        public string Label { get => _label; set => _label = value; }
        /// <summary>
        /// URL of the mod. Can be the Repo url, or the moddb page by example
        /// Example: "https://github.com/0ADMods/mod_test"
        /// </summary>
        public string Url { get => _url; set => _url = value; }
        /// <summary>
        /// Short Description of the mod
        /// Example: A test mod with examples files
        /// </summary>
        public string Description { get => _description; set => _description = value; }
        /// <summary>
        /// A list of strings containing all the dependencies
        /// Example: ["0ad=0.0.23"]
        /// </summary>
        public string[] Dependencies { get => _dependencies; set => _dependencies = value; }
    }
}
