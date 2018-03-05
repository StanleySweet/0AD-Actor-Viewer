using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Uniforms : HashSet<Uniform>, I0adXmlSerializableElement
    {
        public void DeserializeSerializeElements(XElement element)
        {
            throw new NotImplementedException();
        }

        public XElement SerializeElements()
        {
            throw new NotImplementedException();
        }
    }
}
