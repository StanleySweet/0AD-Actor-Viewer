using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public class Prop
    {
        private string attachPoint;
        private string _relativePath;
        private decimal _maxHeight;
        private decimal _minHeight;
        private bool IsSelectable;

        public Prop(XElement prop)
        {
            this._relativePath = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "actor")?.Value;
            this.AttachPoint = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "attachpoint")?.Value;
            decimal.TryParse(prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "minheight")?.Value, out this._minHeight);
            decimal.TryParse(prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "minheight")?.Value, out this._maxHeight);
            bool.TryParse(prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "selectable")?.Value, out this.IsSelectable);
        }

        public bool IsSelectable1 { get => IsSelectable; set => IsSelectable = value; }
        public decimal MinHeight { get => _minHeight; set => _minHeight = value; }
        public string AttachPoint { get => attachPoint; set => attachPoint = value; }

        public string GetRelativePath()
        { return this._relativePath; }

        public void SetRelativePath(string value)
        { this._relativePath = value; }

    }
}
