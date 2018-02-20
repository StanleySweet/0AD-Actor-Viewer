using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    /// <summary>
    /// Defines the animation Tag
    /// </summary>
    public class Animation
    {
        private decimal _eventFrequency;
        private decimal _loadFrequency;
        private decimal _soundGain;
        private string _relativePath;
        private string _name;
        private string _id;
        private uint _animationSpeed;
        private uint _frequency;

        public Animation()
        {

        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="animation"></param>
        public Animation(XElement animation)
        {
            decimal.TryParse(animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "event")?.Value, out _eventFrequency);
            decimal.TryParse(animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "load")?.Value, out _loadFrequency);
            decimal.TryParse(animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "sound")?.Value, out _soundGain);
            uint.TryParse(animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "frequency")?.Value, out _frequency);
            uint.TryParse(animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "speed")?.Value, out _animationSpeed);
            _relativePath = animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "file")?.Value;
            _id = animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "id")?.Value;
            _name = animation.Attributes().FirstOrDefault(a => a.Name.LocalName == "name")?.Value;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool IsChecked { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal EventFrequency { get => _eventFrequency; set => _eventFrequency = value; }
        /// <summary>
        /// 
        /// </summary>
        public decimal LoadFrequency { get => _loadFrequency; set => _loadFrequency = value; }
        /// <summary>
        /// The gain at which the sound will be played.
        /// </summary>
        public decimal SoundGain { get => _soundGain; set => _soundGain = value; }
        /// <summary>
        /// Name of the animation.
        /// </summary>
        public string Name { get => _name; set => _name = value; }
        /// <summary>
        /// The animation string id for synchronisation between props.
        /// </summary>
        public string Id { get => _id; set => _id = value; }
        /// <summary>
        /// The speed of the animation, in percent
        /// </summary>
        public uint AnimationSpeed { get => _animationSpeed; set => _animationSpeed = value; }
        /// <summary>
        /// If there are multiple animations, the ratio between their appearance
        /// </summary>
        public uint Frequency { get => _frequency; set => _frequency = value; }
        /// <summary>
        /// Path to the animation file.
        /// </summary>
        public string RelativePath { get => _relativePath; set => _relativePath = value; }

        internal XElement SerializeElements()
        {
            var curAnim = new XElement("animation");
            if (!string.IsNullOrEmpty(this._name))
                curAnim.Add(new XAttribute("name", this._name));
            if (!string.IsNullOrEmpty(this._id))
                curAnim.Add(new XAttribute("id", this._id));
            if (!string.IsNullOrEmpty(this._relativePath))
                curAnim.Add(new XAttribute("file", this._relativePath));
            if (this._animationSpeed > 0)
                curAnim.Add(new XAttribute("speed", this._animationSpeed));
            if (this._frequency > 0)
                curAnim.Add(new XAttribute("frequency", this._frequency));
            if (this._loadFrequency > 0)
                curAnim.Add(new XAttribute("load", this._loadFrequency));
            if (this._eventFrequency > 0)
                curAnim.Add(new XAttribute("event", this._eventFrequency));
            if (this._soundGain > 0)
                curAnim.Add(new XAttribute("sound", this._soundGain));

            return curAnim;
        }
    }
}
