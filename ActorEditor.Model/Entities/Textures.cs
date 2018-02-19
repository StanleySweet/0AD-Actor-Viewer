﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Textures : HashSet<Texture>
    {
        public Textures()
        {

        }

        public Textures(IEnumerable<XElement> textures) : base()
        {
            if (textures != null)
                foreach (var texture in textures)
                    this.Add(new Texture(texture));
        }

        public override string ToString()
        {
            string serializedString = string.Empty;
            foreach(Texture texture in this)
            {
                serializedString += "(" + texture.GetRelativePath() + ")";
            }
            return serializedString;
        }

        public XElement SerializeElements()
        {
            var textures = new XElement("textures");
            foreach (var texture in this)
            {
                var Xprop = new XElement("texture");
                Xprop.Add(new XAttribute("file", texture.GetRelativePath()));
                Xprop.Add(new XAttribute("name", texture.GetTextureType()));
                textures.Add(Xprop);
            }
            return textures;
        }
    }
}
