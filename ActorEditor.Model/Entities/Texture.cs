using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public enum ETextureType
    {
        baseTex,
        normTex,
        specTex,
        aoTex,
    }
    public class Texture
    {
        private static Dictionary<string, ETextureType> TextureTypeMapper = new Dictionary<string, ETextureType>()
        {
            { "baseTex", ETextureType.baseTex},
            { "specTex", ETextureType.specTex},
            { "normTex", ETextureType.normTex},
            { "aoTex", ETextureType.aoTex}
        };

        private string _relativePath;
        private ETextureType _textureType;

        public Texture()
        {
        }

        public Texture(XElement texture)
        {
            this._relativePath = texture.Attributes().FirstOrDefault(a => a.Name.LocalName == "file")?.Value;
            TextureTypeMapper.TryGetValue(texture.Attributes().FirstOrDefault(a => a.Name.LocalName == "name")?.Value, out this._textureType);
        }

        public Texture(string relativePath, ETextureType textureType = ETextureType.baseTex)
        {
            this._relativePath = relativePath;
            this._textureType = textureType;
        }

        public bool IsChecked { get; set; }
        public string RelativePath { get => _relativePath; set => _relativePath = value; }
        public ETextureType TextureType { get => _textureType; set => _textureType = value; }

        public XElement SerializeElements()
        {
            var Xtexture = new XElement("texture");
            Xtexture.Add(new XAttribute("file", string.IsNullOrEmpty(this._relativePath) ? "null_white.dds" : _relativePath));
            Xtexture.Add(new XAttribute("name", this._textureType));
            return Xtexture;
        }
    }
}
