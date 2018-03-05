using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Constant : I0adXmlSerializableElement
    {
        public const string CONSTANT_NODE_NAME = "constant";
        public const string CONSTANT_NAME_ATTRIBUTE_NAME = "name";
        public const string CONSTANT_VALUE_ATTRIBUTE_NAME = "value";

        private string _name;
        private float _value;

        public Constant()
        {
            _value = 0;
            _name = string.Empty;
        }

        public Constant(XElement constant)
        {
            DeserializeElements(constant);
        }

        public XElement SerializeElements()
        {
            throw new NotImplementedException();
        }

        public void DeserializeElements(XElement element)
        {
            this._name = element.Attributes().FirstOrDefault(a => a.Name.LocalName.Equals(CONSTANT_NAME_ATTRIBUTE_NAME))?.Value;
            float.TryParse(element.Attributes().FirstOrDefault(a => a.Name.LocalName.Equals(CONSTANT_VALUE_ATTRIBUTE_NAME))?.Value, out this._value);
        }

        public string Name { get => _name; set => _name = value; }
        public float Value { get => _value; set => _value = value; }
    }
}
