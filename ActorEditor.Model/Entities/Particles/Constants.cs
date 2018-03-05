using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Constants : HashSet<Constant>
    {
        public Constants(IEnumerable<XElement> textures) : base()
        {
            if (textures != null)
                foreach (var constant in textures)
                    this.Add(new Constant(constant));
        }

        public List<XElement> SerializeElements()
        {
            var constants = new List<XElement>();
            foreach (var constant in this)
                constants.Add(constant.SerializeElements());

            return constants;
        }
    }
}
