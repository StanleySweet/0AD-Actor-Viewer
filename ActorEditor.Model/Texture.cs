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

        public string RelativePath
        {
            get { return this._relativePath; }
            set { this._relativePath = value; }
        }

        public TextureType TextureType
        {
            get { return this._textureType; }
            set { this._textureType = value; }
        }
    }
}
