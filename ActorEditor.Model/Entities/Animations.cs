using System;
using System.Collections.Generic;
using System.Text;

namespace ActorEditor.Model.Entities
{
    public class Animations : HashSet<Animation>
    {
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
