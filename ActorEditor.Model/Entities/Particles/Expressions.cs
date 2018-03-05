using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Entities.Particles
{
    class Expressions : HashSet<Expression>
    {
        public List<XElement> SerializeElements()
        {
            var experssions = new List<XElement>();
            foreach (var constant in this)
                experssions.Add(constant.SerializeElements());

            return experssions;
        }
    }
}
