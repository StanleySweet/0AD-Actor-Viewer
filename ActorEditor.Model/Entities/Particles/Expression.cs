using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Expression : I0adXmlSerializableElement
    {
        private string _name;
        private string _from;
        private float _multiplier;
        private float _max;

        public Expression()
        {

        }

        public Expression(XElement expression)
        {

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
