using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Markdown.Avalonia;

public class HeaderScrolledEventArgs : EventArgs, IEquatable<HeaderScrolledEventArgs>
{
    public HeaderScrolledEventArgs(IList<Header> tree, IList<Header> viewing)
    {
        Tree = new ReadOnlyCollection<Header>(tree);
        Viewing = new ReadOnlyCollection<Header>(viewing);
    }

    public IReadOnlyList<Header> Tree { get; }
    public IReadOnlyList<Header> Viewing { get; }

    public bool Equals(HeaderScrolledEventArgs? other)
    {
        if (other is null)
            return false;

        return Tree.SequenceEqual(other.Tree)
               && Viewing.SequenceEqual(other.Viewing);
    }

    public override int GetHashCode()
    {
        return Tree.Sum(e => e.GetHashCode()) + Viewing.Sum(e => e.GetHashCode());
    }

    public override bool Equals(object? obj)
    {
        return obj is HeaderScrolledEventArgs arg ? Equals(arg) : false;
    }

    public static bool operator !=(HeaderScrolledEventArgs? left, HeaderScrolledEventArgs? right)
    {
        return !(left == right);
    }

    public static bool operator ==(HeaderScrolledEventArgs? left, HeaderScrolledEventArgs? right)
    {
        return left is not null ? left.Equals(right) :
            right is not null ? false :
            true;
    }
}