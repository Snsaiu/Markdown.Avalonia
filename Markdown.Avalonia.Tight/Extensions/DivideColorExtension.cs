using Avalonia;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Markup.Xaml.MarkupExtensions;
using Avalonia.Media;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Markdown.Avalonia.Extensions
{
    public class DivideColorExtension : MarkupExtension
    {
        private readonly string _frmKey;
        private readonly string _toKey;
        private readonly double _relate;

        public DivideColorExtension(string frm, string to, double relate)
        {
            _frmKey = frm;
            _toKey = to;
            _relate = relate;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var left = CreateBinding(_frmKey);
            var right = CreateBinding(_toKey);

            return new MultiBinding
            {
                Bindings = new[] { left, right },
                Converter = new DivideConverter(_relate)
            };
        }

        private Binding CreateBinding(string keyOrColor)
        {
            if (Color.TryParse(keyOrColor, out var color))
            {
                return new Binding
                {
                    Source = new SolidColorBrush( color)
                };
            }

            // 绑定到当前控件的资源字典中的颜色
            return new Binding
            {
                Path = $"[{keyOrColor}]", // 等价于 Resources["key"]
                RelativeSource = new RelativeSource(RelativeSourceMode.Self)
            };
        }
    }


    class DivideConverter : IMultiValueConverter
    {
        public double Relate { get; }

        public DivideConverter(double relate)
        {
            Relate = relate;
        }

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            Color colL;
            if (values[0] is ISolidColorBrush bl)
                colL = bl.Color;
            else if (values[0] is Color cl)
                colL = cl;
            else
                return values[0];

            Color colR;
            if (values[1] is ISolidColorBrush br)
                colR = br.Color;
            else if (values[1] is Color cr)
                colR = cr;
            else
                return values[0];

            static byte Calc(byte l, byte r, double d)
                => (byte)(l * (1 - d) + r * d);

            return new SolidColorBrush(
                        Color.FromArgb(
                            Calc(colL.A, colR.A, Relate),
                            Calc(colL.R, colR.R, Relate),
                            Calc(colL.G, colR.G, Relate),
                            Calc(colL.B, colR.B, Relate)));
        }
    }
}