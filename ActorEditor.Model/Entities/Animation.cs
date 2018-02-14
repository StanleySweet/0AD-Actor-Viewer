using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    public class Animation
    {
        private decimal _eventFrequency;
        private decimal _loadFrequency;
        private decimal _soundGain;
        private string _name;
        private string _id;
        private uint _animationSpeed;
        private uint _frequency;
        private string _relativePath;

        public Animation(XElement animation)
        {

        }
    }
}
