using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Group : HashSet<Variant>
    {
        public bool IsChecked { get; set; }

        public XElement SerializeElements()
        {
            var curGroup = new XElement(Constants.GROUP_ROOT_TAG_NAME);
            foreach (var variant in this)
            {
                curGroup.Add(variant.SerializeElements());
            }
            return curGroup;
        }

        public override string ToString()
        {
            return Constants.GROUP_ROOT_TAG_NAME;
        }
    }
}
