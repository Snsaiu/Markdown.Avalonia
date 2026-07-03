using System;
using Avalonia;
using Avalonia.Media;

namespace ColorTextBlock.Avalonia.Geometries;

public class ImageGeometry : CGeometry
{
    internal ImageGeometry(
        CImage owner,
        IImage image, double width, double height,
        TextVerticalAlignment alignment) :
        base(owner, width, height, height, alignment, false)
    {
        Image = image;
        Width = width;
        Height = height;
    }

    public new double Width { get; }
    public new double Height { get; }
    public IImage Image { get; }

    public override void Render(DrawingContext ctx)
    {
        ctx.DrawImage(
            Image,
            new Rect(Image.Size),
            new Rect(Left, Top, Width, Height));
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