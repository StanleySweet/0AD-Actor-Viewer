using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Props : HashSet<Prop>
    {
        public override string ToString()
        {
            string serializedString = string.Empty;
            foreach (Prop prop in this)
            {
                serializedString += "(" + prop.GetRelativePath() + ", " + prop.AttachPoint +  ")";
            }
            return !string.IsNullOrEmpty(serializedString) ? (serializedString.Substring(0, 10 > serializedString.Length ? 10 : serializedString.Length)) : string.Empty;
        }

        public XElement SerializeElements()
        {
            var props = new XElement("props");
            foreach (var prop in this)
            {
                var Xprop = new XElement("prop");
                Xprop.Add(new XAttribute("attachpoint", prop.AttachPoint));
                Xprop.Add(new XAttribute("actor", prop.GetRelativePath()));
                props.Add(Xprop);
            }
            return props;
        }
    }
}
