using System;
using System.Collections.Generic;
using System.Text;

namespace ActorEditor.Model
{
    class Prop
    {
        private string attachPoint;
        private string _relativePath;
        private decimal _maxHeight;
        private decimal _minHeight;
        private bool IsSelectable;

        public string RelativePath
        {
            get { return this._relativePath; }
            set { this._relativePath = value; }
        }

    }
}
