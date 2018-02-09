using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public class Variant
    {
        private string _name;
        private string _parentVariantRelativePath;
        private uint _frequency;
        private HashSet<Texture> _textures;
        private HashSet<Animation> _animations;
        private HashSet<Prop> _props;
        private Particle _particle;
        private Decal _decal;
        private Color _color;
        private Mesh _mesh;

        public Variant(XDocument variantFile)
        {

        }

        public Variant(XElement variantBlock)
        {
            this._name = variantBlock.Attributes().FirstOrDefault(a => a.Name.LocalName == "name").Value;
            this._parentVariantRelativePath = variantBlock.Attributes().FirstOrDefault(a => a.Name.LocalName == "file")?.Value;
            this._frequency = uint.Parse(variantBlock.Attributes().FirstOrDefault(a => a.Name.LocalName == "frequency")?.Value);

        }
    }
}
