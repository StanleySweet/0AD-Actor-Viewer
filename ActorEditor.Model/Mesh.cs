using System;
using System.Collections.Generic;
using System.Text;

namespace ActorEditor.Model
{
    public class Mesh
    {
        private string _relativePath;

        public Mesh(string relativePath)
        {
            this._relativePath = relativePath;
        }

        public string RelativePath
        {
            get { return this._relativePath; }
            set { this._relativePath = value; }
        }
    }
}
