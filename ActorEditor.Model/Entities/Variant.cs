using ActorEditor.Model.Entities;
using ActorEditor.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public class Variant : I0adXmlSerializableElement
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

        public Variant(XDocument variantFile)
        {
            DeserializeElements(variantFile.Elements().FirstOrDefault(a => a.Name == Constants.VARIANT_ROOT_TAG_NAME));
        }

        public Variant(XElement variant)
        {
            DeserializeElements(variant);
        }

        public string Name { get => _name; set => _name = value; }
        public string ParentVariantRelativePath { get => _parentVariantRelativePath; set => _parentVariantRelativePath = value; }

        public XElement SerializeElements()
        {
            var curVariant = new XElement(Constants.VARIANT_ROOT_TAG_NAME);
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
            if (!string.IsNullOrEmpty(this.Particle))
            {
                var particle = new XElement("particles");
                particle.Add(new XAttribute("file", this._particle));
                curVariant.Add(particle);
            }
                
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

        public void DeserializeElements(XElement element)
        {
            if (!element.Name.LocalName.Equals(Constants.VARIANT_ROOT_TAG_NAME))
                throw new System.TypeLoadException();
            this.Name = element.Attributes().FirstOrDefault(a => a.Name.LocalName == "name")?.Value;
            this.ParentVariantRelativePath = element.Attributes().FirstOrDefault(a => a.Name.LocalName == "file")?.Value;
            this.Mesh = element.Elements().FirstOrDefault(a => a.Name.LocalName == "mesh")?.Value;
            this.Particle = element.Elements().FirstOrDefault(a => a.Name.LocalName == "particles")?.Attributes().FirstOrDefault(a => a.Name.LocalName == "file").Value;
            this.Color = new ActorColor(element.Elements().FirstOrDefault(a => a.Name.LocalName == "color"));

            uint.TryParse(element.Attributes().FirstOrDefault(a => a.Name.LocalName == "frequency")?.Value, out this._frequency);
            if (string.IsNullOrEmpty(this.Name) && string.IsNullOrEmpty(this.ParentVariantRelativePath))
            {
                Name = "Base";
                if (Frequency < 1)
                    Frequency = 1;
            }

            _props = new Props(element.Elements().FirstOrDefault(x => x.Name.LocalName == "props")?.Elements());
            _textures = new Textures(element.Elements().FirstOrDefault(x => x.Name.LocalName == "textures")?.Elements());
            _animations = new Animations(element.Elements().FirstOrDefault(x => x.Name.LocalName == "animations")?.Elements());
        }
    }
}
