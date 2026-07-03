using System;
using System.Collections;
using System.Collections.Generic;

namespace ColorDocument.Avalonia;

public class SelectionList : IList<DocumentElement>
{
    private readonly IList<DocumentElement> _elements;
    private SelectRange _range;

    public SelectionList(SelectDirection direction, SelectRange range, IList<DocumentElement> elements)
    {
        Direction = direction;
        _range = range;
        _elements = elements;
    }

    public SelectDirection Direction { get; }

    public DocumentElement this[int index]
    {
        get => _elements[index];
        set => throw new InvalidOperationException();
    }

    public int Count => _elements.Count;

    public bool IsReadOnly => true;

    public void Add(DocumentElement item)
    {
        throw new InvalidOperationException();
    }

    public void Clear()
    {
        throw new InvalidOperationException();
    }

    public bool Contains(DocumentElement item)
    {
        return _elements.Contains(item);
    }

    public void CopyTo(DocumentElement[] array, int arrayIndex)
    {
        _elements.CopyTo(array, arrayIndex);
    }

    public IEnumerator<DocumentElement> GetEnumerator()
    {
        return _elements.GetEnumerator();
    }

    public int IndexOf(DocumentElement item)
    {
        return _elements.IndexOf(item);
    }

    public void Insert(int index, DocumentElement item)
    {
        throw new InvalidOperationException();
    }

    public bool Remove(DocumentElement item)
    {
        throw new InvalidOperationException();
    }

    public void RemoveAt(int index)
    {
        throw new InvalidOperationException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

public enum SelectDirection
{
    Forward,
    Backward
}

public enum SelectRange
{
    Part = 0b0001,
    Begin = 0b0011,
    End = 0b0101,
    Fill = 0b0111
}