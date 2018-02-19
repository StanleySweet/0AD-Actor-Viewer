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


        public override string ToString()
        {
            string serializedString = string.Empty;
            foreach (Animation animation in this)
            {
                serializedString += "(" + ")";
            }
            return serializedString;
        }
    }
}
