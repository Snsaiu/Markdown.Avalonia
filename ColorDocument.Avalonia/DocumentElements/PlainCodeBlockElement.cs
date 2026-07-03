using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;

namespace ColorDocument.Avalonia.DocumentElements;

public class PlainCodeBlockElement : DocumentElement
{
    private readonly Lazy<Border> _border;
    private readonly string _code;

    public PlainCodeBlockElement(string code)
    {
        _code = code;
        _border = new Lazy<Border>(CreateBlock);
    }

    public override Control Control => _border.Value;

    public override IEnumerable<DocumentElement> Children => Array.Empty<DocumentElement>();

    public override void Select(Point from, Point to)
    {
    }

    public override void UnSelect()
    {
    }

    public Border CreateBlock()
    {
        var ctxt = new TextBlock
        {
            Text = _code,
            TextWrapping = TextWrapping.NoWrap
        };
        ctxt.Classes.Add(ClassNames.CodeBlockClass);

        var scrl = new ScrollViewer();
        scrl.Classes.Add(ClassNames.CodeBlockClass);
        scrl.Content = ctxt;
        scrl.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;

        var result = new Border();
        result.Classes.Add(ClassNames.CodeBlockClass);
        result.Child = scrl;

        return result;
    }

    public override void ConstructSelectedText(StringBuilder stringBuilder)
    {
        stringBuilder.Append(_code);
    }
}