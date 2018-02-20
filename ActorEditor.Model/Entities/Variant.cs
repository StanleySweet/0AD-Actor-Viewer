using ActorEditor.Model.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public class Variant
    {
        public bool IsChecked { get; set; }

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

        public Variant()
        {
            _props = new Props();
            _textures = new Textures();
            _animations = new Animations();
            _color = new ActorColor(null);
        }

        public void Deserialize(XElement variant)
        {
            this.Name = variant.Attributes().FirstOrDefault(a => a.Name.LocalName == "name")?.Value;
            this.ParentVariantRelativePath = variant.Attributes().FirstOrDefault(a => a.Name.LocalName == "file")?.Value;
            this.Mesh = variant.Elements().FirstOrDefault(a => a.Name.LocalName == "mesh")?.Value;
            this.Particle = variant.Elements().FirstOrDefault(a => a.Name.LocalName == "particles")?.Value;
            this.Color = new ActorColor(variant.Elements().FirstOrDefault(a => a.Name.LocalName == "color"));

            uint.TryParse(variant.Attributes().FirstOrDefault(a => a.Name.LocalName == "frequency")?.Value, out this._frequency);
            if (string.IsNullOrEmpty(this.Name) && string.IsNullOrEmpty(this.ParentVariantRelativePath))
            {
                Name = "Base";
                if (Frequency < 1)
                    Frequency = 1;
            }

            _props = new Props(variant.Elements().FirstOrDefault(x => x.Name.LocalName == "props")?.Elements());
            _textures = new Textures(variant.Elements().FirstOrDefault(x => x.Name.LocalName == "textures")?.Elements());
            _animations = new Animations(variant.Elements().FirstOrDefault(x => x.Name.LocalName == "animations")?.Elements());

        }

        public Variant(XDocument variantFile)
        {
            Deserialize(variantFile.Elements().FirstOrDefault(a => a.Name == "variant"));

        }

        public Variant(XElement variant)
        {
            Deserialize(variant);
        }

        public string Name { get => _name; set => _name = value; }
        public string ParentVariantRelativePath { get => _parentVariantRelativePath; set => _parentVariantRelativePath = value; }

        internal XElement SerializeElements()
        {
            var curVariant = new XElement("variant");
            if (!string.IsNullOrEmpty(this.ParentVariantRelativePath))
                curVariant.Add(new XAttribute("file", this.ParentVariantRelativePath));
            if (this.Frequency > 0)
                curVariant.Add(new XAttribute("frequency", this.Frequency));
            if (!string.IsNullOrEmpty(this.Name))
                curVariant.Add(new XAttribute("name", this.Name));
            if (this.Animations.Count > 0)
                curVariant.Add(this.Animations.SerializeElements());
            if (!string.IsNullOrEmpty(this.Color.ToString()))
                curVariant.Add(new XElement("color", this.Color.ToString()));
            if (!string.IsNullOrEmpty(this.Mesh))
                curVariant.Add(new XElement("mesh", this.Mesh));
            if (this.Props.Count > 0)
                curVariant.Add(this.Props.SerializeElements());
            if (this.Textures.Count > 0)
                curVariant.Add(this.Textures.SerializeElements());
            return curVariant;
        }

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
