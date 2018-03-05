using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Uniform : I0adXmlSerializableElement
    {
        string _name;
        int _min;
        int _max;

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
