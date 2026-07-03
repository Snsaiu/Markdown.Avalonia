using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;

namespace Markdown.Avalonia.Html.Tables;

internal class TableCell
{
    public TableCell()
    {
        RowSpan = 1;
        ColSpan = 1;
        Horizontal = null;
        Vertical = null;
        Content = new Border();
    }

    public TableCell(IEnumerable<Control> controls) : this()
    {
        var ctrls = controls.ToArray();
        switch (ctrls.Length)
        {
            case 0:
                break;

            case 1:
                Content.Child = ctrls[0];
                break;

            default:
                var panel = new StackPanel { Orientation = Orientation.Vertical };
                panel.Children.AddRange(ctrls);

                Content.Child = panel;
                break;
        }
    }

    public int ColumnIndex { get; set; }

    public Border Content { get; set; }
    public int RowSpan { get; set; }
    public int ColSpan { get; set; }
    public TextAlignment? Horizontal { get; set; }
    public VerticalAlignment? Vertical { get; set; }
}