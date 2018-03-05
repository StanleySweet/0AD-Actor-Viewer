

namespace ActorEditor.Model
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Linq;
    using ActorEditor.Model.Entities;
    using ActorEditor.Model.Interfaces;

    public class Actor : I0adXmlSerializableElement
    {
        private uint _version;
        private bool _castsShadows;
        private bool _floats;

        private Groups _groups;
        private string _material;

        public Actor()
        {
            _groups = new Groups();
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="actorFile"></param>
        public Actor(XDocument actorFile)
        {        
            _groups = new Groups();
            DeserializeElements(actorFile.Elements().FirstOrDefault(a => a.Name == "actor"));
        }

        public uint Version { get => _version; set => _version = value; }
        public bool Floats { get => _floats; set => _floats = value; }
        public bool CastsShadows { get => _castsShadows; set => _castsShadows = value; }
        public string Material { get => _material; set => _material = value; }
        public Groups Groups { get => _groups; set => _groups = value; }

        public void DeserializeElements(XElement element)
        {
            if (!element.Name.LocalName.Equals("actor"))
                throw new System.TypeLoadException();

            uint.TryParse(element.Attributes().FirstOrDefault(a => a.Name.LocalName == "version")?.Value, out this._version);
            this._castsShadows = element.Elements().FirstOrDefault(a => a.Name.LocalName == "castshadow") != null;
            this._floats = element.Elements().FirstOrDefault(a => a.Name.LocalName == "float") != null;
            this._material = element.Elements().FirstOrDefault(a => a.Name.LocalName == "material")?.Value;

            var xElementGroups = element.Elements().Where(a => a.Name.LocalName == Constants.GROUP_ROOT_TAG_NAME);
            foreach (var xElementGroup in xElementGroups)
            {
                var group = new Group();
                var xElementVariants = xElementGroup.Elements().Where(a => a.Name.LocalName == Constants.VARIANT_ROOT_TAG_NAME);
                foreach (var xElementVariant in xElementVariants)
                    group.Add(new Variant(xElementVariant));

                _groups.Add(group);
            }
        }

        public XElement SerializeElements()
        {
            XElement root = new XElement("actor");
            root.Add(new XAttribute("version", this.Version));

            if (this.CastsShadows)
                root.Add(new XElement("castshadow"));
            if (this.Floats)
                root.Add(new XElement("float"));

            foreach (var group in this.Groups)
                root.Add(group.SerializeElements());
            if (!string.IsNullOrEmpty(Material))
                root.Add(new XElement("material", this.Material));
            else
                root.Add(new XElement("material", "default.xml"));

            return root;
        }
    }
}
