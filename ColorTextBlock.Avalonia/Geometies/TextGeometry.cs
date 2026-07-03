using Avalonia.Media;

namespace ColorTextBlock.Avalonia.Geometries;

internal abstract class TextGeometry : CGeometry
{
    internal TextGeometry(
        CInline owner,
        double width, double height, double lineHeight,
        TextVerticalAlignment alignment,
        bool linebreak) :
        base(owner, width, height, lineHeight, alignment, linebreak)
    {
    }

    public IBrush? TemporaryForeground { get; set; }

    public IBrush? TemporaryBackground { get; set; }

    public IBrush? Foreground => Owner?.Foreground;

    public IBrush? Background => Owner?.Background;

    public bool IsUnderline => Owner is null ? false : Owner.IsUnderline;

    public bool IsStrikethrough => Owner is null ? false : Owner.IsStrikethrough;
}