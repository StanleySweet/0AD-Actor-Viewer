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
        private bool _IsSelectable;

        public Prop()
        {

        }

        public Prop(XElement prop)
        {
            this._relativePath = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "actor")?.Value;
            this.AttachPoint = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "attachpoint")?.Value;
            decimal.TryParse(prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "minheight")?.Value, out this._minHeight);
            decimal.TryParse(prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "maxheight")?.Value, out this._maxHeight);
            bool.TryParse(prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "selectable")?.Value, out this._IsSelectable);
        }

        internal XElement SerializeElements()
        {
            var Xprop = new XElement("prop");
            if (!string.IsNullOrEmpty(this.attachPoint))
                Xprop.Add(new XAttribute("attachpoint", this.attachPoint));
            if (!string.IsNullOrEmpty(this._relativePath))
                Xprop.Add(new XAttribute("actor", this._relativePath));
            if (_IsSelectable)
                Xprop.Add(new XAttribute("selectable", 1));
            if (_minHeight != 0)
                Xprop.Add(new XAttribute("minheight", _minHeight));
            if (_maxHeight != 0)
                Xprop.Add(new XAttribute("maxheight", _maxHeight));
            return Xprop;
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked { get; set; }
        public bool IsSelectable { get => _IsSelectable; set => _IsSelectable = value; }
        public decimal MinHeight { get => _minHeight; set => _minHeight = value; }
        public string AttachPoint { get => attachPoint; set => attachPoint = value; }
        public string RelativePath { get => _relativePath; set => _relativePath = value; }
        public decimal MaxHeight { get => _maxHeight; set => _maxHeight = value; }
    }
}
