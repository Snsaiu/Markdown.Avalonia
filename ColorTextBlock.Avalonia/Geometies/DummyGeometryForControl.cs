using System;
using Avalonia.Controls;
using Avalonia.Media;
using ColorTextBlock.Avalonia.Geometries;

namespace ColorTextBlock.Avalonia.Geometies;

internal class DummyGeometryForControl : CGeometry
{
    public DummyGeometryForControl(CInlineUIContainer owner, Control control, TextVerticalAlignment alignment) :
        base(
            owner,
            control.DesiredSize.Width,
            control.DesiredSize.Height,
            control.DesiredSize.Height,
            alignment,
            false)
    {
        Control = control;
    }

    public Control Control { get; }

    public override void Render(DrawingContext ctx)
    {
    }

    public override TextPointer CalcuatePointerFrom(double x, double y)
    {
        if (x < Left + Width / 2) return GetBegin();

        return GetEnd();
    }

    public override TextPointer CalcuatePointerFrom(int index)
    {
        return index switch
        {
            0 => GetBegin(),
            1 => GetEnd(),
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };
    }

    public override TextPointer GetBegin()
    {
        return new TextPointer(this);
    }

    public override TextPointer GetEnd()
    {
        return new TextPointer(this, 1, Width);
    }
}