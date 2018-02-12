using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace ActorEditor.Model
{
    class ActorColor
    {
        byte _red, _blue, _green;
        bool _hasColor;

        public ActorColor(Color color)
        {
            _red = color.R;
            _blue = color.B;
            _green = color.R;
        }

        public ActorColor(XElement color)
        {
            if (color != null)
            {
                _red = byte.Parse(color.Value.Split(' ')[0]);
                _blue = byte.Parse(color.Value.Split(' ')[1]);
                _green = byte.Parse(color.Value.Split(' ')[2]);
                _hasColor = true;
            }
            else
                _hasColor = false;
        }

        public void SetColor(Color color)
        {
            _red = color.R;
            _blue = color.B;
            _green = color.R;
        }

        public Color GetColor(Color color)
        {
            return Color.FromArgb(_red, _green, _blue);
        }

        public override string ToString()
        {
            return _hasColor ?( _red + " " + _green + " " + _blue) : "";
        }
    }
}
