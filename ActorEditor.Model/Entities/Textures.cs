using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Textures : HashSet<Texture>
    {
        public Textures()
        {

        }

        public Textures(IEnumerable<XElement> textures) : base()
        {
            if (textures != null)
                foreach (var texture in textures)
                    this.Add(new Texture(texture));
        }

        public override string ToString() => (this.Count > 0 ? this.Count.ToString() + " texture(s)" : "No texture");

        public XElement SerializeElements()
        {
            var textures = new XElement("textures");
            foreach (var texture in this)
                textures.Add(texture.SerializeElements());
            
            return textures;
        }
    }
}
