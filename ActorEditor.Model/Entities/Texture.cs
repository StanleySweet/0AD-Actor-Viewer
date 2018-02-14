using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public enum TextureType
    {
        baseTex,
        normTex,
        specTex,
        aoTex,
    }
    public class Texture
    {
        public static Dictionary<string, TextureType> TextureTypeMapper = new Dictionary<string, TextureType>()
        {
            { "baseTex", TextureType.baseTex},
            { "specTex", TextureType.specTex},
            { "normTex", TextureType.normTex},
            { "aoTex", TextureType.aoTex}
        };



        private string _relativePath;
        private TextureType _textureType;

        public Texture(XElement texture)
        {
            this._relativePath = texture.Attributes().FirstOrDefault(a => a.Name.LocalName == "file")?.Value;
            TextureTypeMapper.TryGetValue(texture.Attributes().FirstOrDefault(a => a.Name.LocalName == "name")?.Value, out this._textureType);
        }

        public Texture(string relativePath, TextureType textureType = TextureType.baseTex)
        {
            this._relativePath = relativePath;
            this._textureType = textureType;
        }

        public string GetRelativePath()
        { return this._relativePath; }

        public void SetRelativePath(string value)
        { this._relativePath = value; }

        public TextureType GetTextureType()
        { return this._textureType; }

        public void SetTextureType(TextureType value)
        { this._textureType = value; }
    }
}
