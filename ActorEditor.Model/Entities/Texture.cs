using System;
using System.Collections.Generic;
using System.Text;

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
        private string _relativePath;
        private TextureType _textureType;

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
