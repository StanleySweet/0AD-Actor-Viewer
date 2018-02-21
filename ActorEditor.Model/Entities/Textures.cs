using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Textures : HashSet<Texture>
    {
        public static readonly int MAX_NUMBER_OF_TEXTURES = 4;

        /// <summary>
        /// Add an element to the collection.
        /// </summary>
        /// <param name="texture"></param>
        public new void Add(Texture texture)
        {
            if(this.Count < MAX_NUMBER_OF_TEXTURES)
                base.Add(texture);
        }

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
