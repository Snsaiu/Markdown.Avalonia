using System;

namespace Markdown.Avalonia;

public class Header : IEquatable<Header>
{
    public Header(int lv, string txt)
    {
        Level = lv;
        Text = txt;
    }

    public int Level { get; }
    public string Text { get; }

    public bool Equals(Header? other)
    {
        return Level == other.Level && Text == other.Text;
    }

    public override int GetHashCode()
    {
        return Level + Text.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is Header arg ? Equals(arg) : false;
    }

    public static bool operator !=(Header? left, Header? right)
    {
        return !(left == right);
    }

    public static bool operator ==(Header? left, Header? right)
    {
        return left is not null ? left.Equals(right) :
            right is not null ? false :
            true;
    }
}