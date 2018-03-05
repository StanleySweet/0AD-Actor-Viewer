using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Copy : I0adXmlSerializableElement
    {
        string _name;
        string _from;

        public Copy()
        {

        }

        public Copy(XElement copy)
        {
            DeserializeElements(copy);
        }

        public void DeserializeElements(XElement element)
        {
            throw new NotImplementedException();
        }

        public XElement SerializeElements()
        {
            throw new NotImplementedException();
        }
    }
}
