using System;
using System.Collections.Generic;
using System.Text;

namespace ActorEditor.Model.Entities.Mod
{

    public class ModJsonFile
    {

        /// <summary>
        /// Internal name of the mod, should not contain special characters
        /// Example : mod_test
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// Version of the mod for compatibility checks
        /// Example: 0.0.23
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// Real name of the mod
        /// Example: Test Mod
        /// </summary>
        public string label { get; set; }
        /// <summary>
        /// URL of the mod. Can be the Repo url, or the moddb page by example
        /// Example: "https://github.com/0ADMods/mod_test"
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// Short Description of the mod
        /// Example: A test mod with examples files
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// A list of strings containing all the dependencies
        /// Example: ["0ad=0.0.23"]
        /// </summary>
        public List<string> dependencies { get; set; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public ModJsonFile()
        {

        }
    }
}
