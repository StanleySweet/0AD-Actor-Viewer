using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    class Prop
    {
        private string attachPoint;
        private string _relativePath;
        private decimal _maxHeight;
        private decimal _minHeight;
        private bool IsSelectable;

        public Prop(XElement prop)
        {
            try
            {
                this._relativePath = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "actor")?.Value;
                this.AttachPoint = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "attachpoint")?.Value;
                //this._maxHeight = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "minheight")?.Value;
                //this._minHeight = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "maxheight")?.Value;
                //this.IsSelectable = prop.Attributes().FirstOrDefault(a => a.Name.LocalName == "selectable")?.Value;
            }
            catch (Exception ex)
            {

                Console.WriteLine("Bug" + ex.Message);
            }

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
