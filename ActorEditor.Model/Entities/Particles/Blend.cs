using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    public enum EBlendMode
    {
        add,
        substract,
        over,
        multiply
    }

    class Blend : I0adXmlSerializableElement
    {
        public const string BLEND_NODE_NAME = "blend";
        public const string BLEND_MODE_ATTRIBUTE_NAME = "mode";
        public const string BLEND_MODE_ADD = "add";
        public const string BLEND_MODE_MULTIPLY = "multiply";
        public const string BLEND_MODE_OVER = "over";
        public const string BLEND_MODE_SUBSTRACT = "substract";

        private static Dictionary<string, EBlendMode> BlendModeMapper = new Dictionary<string, EBlendMode>()
        {
            { BLEND_MODE_ADD, EBlendMode.add},
            { BLEND_MODE_SUBSTRACT, EBlendMode.substract},
            { BLEND_MODE_OVER, EBlendMode.over},
            { BLEND_MODE_MULTIPLY, EBlendMode.multiply}
        };

        EBlendMode _mode;

        public Blend()
        {

        }

        public Blend(XElement blend)
        {
            DeserializeElements(blend);
        }

        public void DeserializeElements(XElement element)
        {
            BlendModeMapper.TryGetValue(element.Attributes().FirstOrDefault(a => BLEND_NODE_NAME.Equals(a.Name.LocalName))?.Value, out this._mode);
        }

        public XElement SerializeElements()
        {
            throw new NotImplementedException();
        }

        public EBlendMode Mode { get => _mode; set => _mode = value; }
    }
}
