

namespace ActorEditor.Model
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Linq;
    using ActorEditor.Model.Entities;

    public class Actor
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

            XElement actor = actorFile.Elements().FirstOrDefault(a => a.Name == "actor");
            this.Version = uint.Parse(actor.Attributes().FirstOrDefault(a => a.Name.LocalName == "version")?.Value);
            this._castsShadows = actor.Elements().FirstOrDefault(a => a.Name.LocalName == "castshadow") != null;
            this._floats = actor.Elements().FirstOrDefault(a => a.Name.LocalName == "float") != null;
            this._material = actor.Elements().FirstOrDefault(a => a.Name.LocalName == "material")?.Value;

            var xElementGroups = actor.Elements().Where(a => a.Name.LocalName == "group");
            foreach (var xElementGroup in xElementGroups)
            {
                var variants = new Group();
                var xElementVariants = xElementGroup.Elements().Where(a => a.Name.LocalName == "variant");
                foreach (var xElementVariant in xElementVariants)
                    variants.Add(new Variant(xElementVariant));

                _groups.Add(variants);
            }
        }

        public uint Version { get => _version; set => _version = value; }
        public bool Floats { get => _floats; set => _floats = value; }
        public bool CastsShadows { get => _castsShadows; set => _castsShadows = value; }
        public string Material { get => _material; set => _material = value; }
        public Groups Groups { get => _groups; set => _groups = value; }

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
