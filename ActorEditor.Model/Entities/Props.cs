using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Props : HashSet<Prop>
    {
        public Props() : base()
        {
        }

        public Props(IEnumerable<XElement> props) : base()
        {
            if (props != null)
                foreach (var prop in props)
                    this.Add(new Prop(prop));
        }

        public override string ToString() => (this.Count > 0 ? this.Count.ToString() + " prop(s)" : "No prop");

        public XElement SerializeElements()
        {
            var props = new XElement("props");
            foreach (var prop in this)
            {
                props.Add(prop.SerializeElements());
            }
            return props;
        }
    }
}
