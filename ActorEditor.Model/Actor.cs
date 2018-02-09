

namespace ActorEditor.Model
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using System.Linq;

    public class Actor
    {
        private uint _version;
        private bool _castsShadows;
        private bool _floats;

        private List<HashSet<Variant>> _groups;
        private Material _material;

        public Actor(XDocument actorFile)
        {
            _groups = new List<HashSet<Variant>>();

            XElement actor = actorFile.Elements().FirstOrDefault(a => a.Name == "actor");
            this.Version = uint.Parse(actor.Attributes().FirstOrDefault(a => a.Name.LocalName == "version").Value);
            this._castsShadows = actor.Elements().FirstOrDefault(a => a.Name.LocalName == "castshadow") != null;
            this._floats = actor.Elements().FirstOrDefault(a => a.Name.LocalName == "float") != null;
            this._material = new Material(actor.Elements().FirstOrDefault(a => a.Name.LocalName == "material").Value);

            var xElementGroups = actor.Elements().Where(a => a.Name.LocalName == "group");
            foreach (var xElementGroup in xElementGroups)
            {
                var variants = new HashSet<Variant>();
                var xElementVariants = xElementGroup.Elements().Where(a => a.Name.LocalName == "variant");
                foreach (var xElementVariant in xElementVariants)
                {
                    variants.Add(new Variant(xElementVariant));
                }
                _groups.Add(variants);
            }
        }

        public uint Version { get => _version; set => _version = value; }
        public bool Floats { get => _floats; set => _floats = value; }
        public bool CastsShadows { get => _castsShadows; set => _castsShadows = value; }
        public Material Material { get => _material; set => _material = value; }
    }
}
