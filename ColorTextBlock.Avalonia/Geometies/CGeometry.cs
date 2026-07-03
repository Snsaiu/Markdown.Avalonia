using System;
using Avalonia.Controls;
using Avalonia.Media;

namespace ColorTextBlock.Avalonia.Geometries;

public abstract class CGeometry : ITextPointerHandleable
{
    private int? _caretLength;

    public CGeometry(
        CInline owner,
        double width, double height, double baseHeight,
        TextVerticalAlignment textVerticalAlignment,
        bool linebreak)
    {
        Owner = owner;
        Width = width;
        Height = height;
        BaseHeight = baseHeight;
        TextVerticalAlignment = textVerticalAlignment;
        LineBreak = linebreak;
    }

    public CInline Owner { get; }
    public double Left { get; set; }
    public double Top { get; set; }
    public double Width { get; }
    public double Height { get; }
    public double BaseHeight { get; }
    public bool LineBreak { get; }
    public TextVerticalAlignment TextVerticalAlignment { get; }

    public virtual Action<Control>? OnMouseEnter { get; set; }
    public virtual Action<Control>? OnMouseLeave { get; set; }
    public virtual Action<Control>? OnMousePressed { get; set; }
    public virtual Action<Control>? OnMouseReleased { get; set; }
    public virtual Action<Control>? OnClick { get; set; }

    public virtual int CaretLength
    {
        get
        {
            if (!_caretLength.HasValue)
                _caretLength = GetEnd().Index - GetBegin().Index;

            return _caretLength.Value;
        }
    }

    public abstract TextPointer CalcuatePointerFrom(int index);
    public abstract TextPointer CalcuatePointerFrom(double x, double y);
    public abstract TextPointer GetBegin();
    public abstract TextPointer GetEnd();

    public event Action? RepaintRequested;

    public abstract void Render(DrawingContext ctx);

    internal void RequestRepaint()
    {
        RepaintRequested?.Invoke();
    }

    public virtual void Arranged()
    {
    }
}