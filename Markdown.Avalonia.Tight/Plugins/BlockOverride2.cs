using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using ColorDocument.Avalonia;
using Markdown.Avalonia.Parsers;

namespace Markdown.Avalonia.Plugins;

public abstract class BlockOverride2 : IBlockOverride
{
    public BlockOverride2(string parserName)
    {
        ParserName = parserName;
    }

    public string ParserName { get; }

    public IEnumerable<Control>? Convert(string text, Match firstMatch, ParseStatus status, IMarkdownEngine engine, out int parseTextBegin, out int parseTextEnd)
    {
        if (Convert2(text, firstMatch, status, engine.Upgrade(), out parseTextBegin, out parseTextEnd) is { } docs) return docs.Select(d => d.Control);
        return null;
    }

    public abstract IEnumerable<DocumentElement>? Convert2(string text, Match firstMatch, ParseStatus status, IMarkdownEngine2 engine, out int parseTextBegin, out int parseTextEnd);
}