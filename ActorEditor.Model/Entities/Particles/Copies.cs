using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Copies : HashSet<Copy>
    {
        public Copies(IEnumerable<XElement> textures) : base()
        {
            if (textures != null)
                foreach (var constant in textures)
                    this.Add(new Copy(constant));
        }

        public List<XElement> SerializeElements()
        {
            var copies = new List<XElement>();
            foreach (var constant in this)
                copies.Add(constant.SerializeElements());

            return copies;
        }
    }
}
