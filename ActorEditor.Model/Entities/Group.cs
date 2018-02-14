using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Group : HashSet<Variant>
    {
        public XElement SerializeElements()
        {
            var curGroup = new XElement("group");
            foreach (var variant in this)
            {
                var curVariant = new XElement("variant");
                if (!string.IsNullOrEmpty(variant.Name))
                    curVariant.Add(new XAttribute("name", variant.Name));
                if (!string.IsNullOrEmpty(variant.ParentVariantRelativePath))
                    curVariant.Add(new XAttribute("file", variant.ParentVariantRelativePath));
                if (variant.Frequency > 0)
                    curVariant.Add(new XAttribute("frequency", variant.Frequency));
                if (!string.IsNullOrEmpty(variant.Mesh))
                    curVariant.Add(new XElement("mesh", variant.Mesh));
                if (!string.IsNullOrEmpty(variant.Color.ToString()))
                    curVariant.Add(new XElement("color", variant.Color.ToString()));
                if (variant.Props.Count > 0)
                    curVariant.Add(variant.Props.SerializeElements());
                if (variant.Textures.Count > 0)
                    curVariant.Add(variant.Textures.SerializeElements());
                curGroup.Add(curVariant);
            }
            return curGroup;
        }

    }
}
