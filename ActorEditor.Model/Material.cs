using System;
using System.Collections.Generic;
using System.Text;

namespace ActorEditor.Model
{
    public class Material
    {
        private string _fileName;

        public Material(string relativePath)
        {
            this._fileName = relativePath;
        }

        public string FileName
        {
            get { return this._fileName; }
            set { this._fileName = value; }
        }
    }
}
