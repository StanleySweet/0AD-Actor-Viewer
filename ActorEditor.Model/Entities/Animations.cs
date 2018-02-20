using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities
{
    public class Animations : HashSet<Animation>
    {
        public Animations() : base()
        {
        }

        public Animations(IEnumerable<XElement> animations) : base()
        {
            if (animations != null)
                foreach (var animation in animations)
                    this.Add(new Animation(animation));
        }

        public XElement SerializeElements()
        {
            var curAnimationGroup = new XElement("animations");
            foreach (var animation in this)
            {
                curAnimationGroup.Add(animation.SerializeElements());
            }
            return curAnimationGroup;
        }


        public override string ToString() => (this.Count > 0 ? this.Count.ToString() + " animations(s)" : "No animation");
    }
}
