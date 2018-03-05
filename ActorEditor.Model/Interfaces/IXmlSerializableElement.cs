using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model.Interfaces
{
    /// <summary>
    /// Defines a 0ad element interface
    /// </summary>
    public interface I0adXmlSerializableElement
    {
        /// <summary>
        /// Set all object's properities from an xml element
        /// </summary>
        /// <param name="element"></param>
        void DeserializeSerializeElements(XElement element);
        /// <summary>
        /// Serialize all object's properties into an xml element;
        /// </summary>
        /// <returns></returns>
        XElement SerializeElements();
    }
}
