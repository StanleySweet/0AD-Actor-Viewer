using ActorEditor.Model.Entities;
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
        private Textures _textures;
        private Animations _animations;
        private Props _props;
        private string _particle;
        private Decal _decal;
        private ActorColor _color;
        private string _mesh;

        public Variant(XDocument variantFile)
        {

        }

        public Variant(XElement variantBlock)
        {


            this.Name = variantBlock.Attributes().FirstOrDefault(a => a.Name.LocalName == "name")?.Value;
            this.ParentVariantRelativePath = variantBlock.Attributes().FirstOrDefault(a => a.Name.LocalName == "file")?.Value;
            uint.TryParse(variantBlock.Attributes().FirstOrDefault(a => a.Name.LocalName == "frequency")?.Value, out this._frequency);

            if (string.IsNullOrEmpty(this.Name) && string.IsNullOrEmpty(this.ParentVariantRelativePath))
            {
                Name = "Base";
                if (Frequency < 1)
                    Frequency = 1;
            }

            _props = new Props();
            var props = variantBlock.Elements().FirstOrDefault(x => x.Name.LocalName == "props")?.Elements();
            if(props != null)
            {
                foreach (var prop in props)
                {
                    _props.Add(new Prop(prop));
                }
            }
            _textures = new Textures();
            var textures = variantBlock.Elements().FirstOrDefault(x => x.Name.LocalName == "textures")?.Elements();
            if (textures != null)
            {
                foreach (var texture in textures)
                {
                    _textures.Add(new Texture(texture));
                }
            }

            _animations = new Animations();
            var animations = variantBlock.Elements().FirstOrDefault(x => x.Name.LocalName == "animations")?.Elements();
            if (animations != null)
            {
                foreach (var animation in animations)
                {
                    _animations.Add(new Animation(animation));
                }
            }

            this.Mesh = variantBlock.Elements().FirstOrDefault(a => a.Name.LocalName == "mesh")?.Value;
            this.Particle = variantBlock.Elements().FirstOrDefault(a => a.Name.LocalName == "particles")?.Value;
            this.Color = new ActorColor(variantBlock.Elements().FirstOrDefault(a => a.Name.LocalName == "color"));
        }

        public string Name { get => _name; set => _name = value; }
        public string ParentVariantRelativePath { get => _parentVariantRelativePath; set => _parentVariantRelativePath = value; }
        public uint Frequency { get => _frequency; set => _frequency = value; }
        public string Mesh { get => _mesh; set => _mesh = value; }
        public string Particle { get => _particle; set => _particle = value; }
        public Textures Textures { get => _textures; set => _textures = value; }
        internal ActorColor Color { get => _color; set => _color = value; }
        internal Decal Decal { get => _decal; set => _decal = value; }
        public Props Props { get => _props; set => _props = value; }
        public Animations Animations { get => _animations; set => _animations = value; }
        public XElement GetVariants()
        {
            return null;
        }
    }
}
