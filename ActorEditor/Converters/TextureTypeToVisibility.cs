using ActorEditor.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ActorEditor.Converters
{
    class TextureTypeToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,  CultureInfo culture)
        {
            return ((ETextureType)value);
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
