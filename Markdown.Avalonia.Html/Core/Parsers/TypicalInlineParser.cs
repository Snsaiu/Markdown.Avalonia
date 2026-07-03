using System.Collections.Generic;
using System.Linq;
using Avalonia;
using ColorTextBlock.Avalonia;
using HtmlAgilityPack;

namespace Markdown.Avalonia.Html.Core.Parsers;

public class TypicalInlineParser : IInlineTagParser
{
    private const string _resource = "Markdown.Avalonia.Html.Core.Parsers.TypicalInlineParser.tsv";
    private readonly TypicalParseInfo _parser;

    public TypicalInlineParser(TypicalParseInfo parser)
    {
        _parser = parser;
    }

    public IEnumerable<string> SupportTag => new[] { _parser.HtmlTag };

    bool ITagParser.TryReplace(HtmlNode node, ReplaceManager manager, out IEnumerable<StyledElement> generated)
    {
        var rtn = _parser.TryReplace(node, manager, out var list);
        generated = list;
        return rtn;
    }

    public bool TryReplace(HtmlNode node, ReplaceManager manager, out IEnumerable<CInline> generated)
    {
        var rtn = _parser.TryReplace(node, manager, out var list);
        generated = list.Cast<CInline>();
        return rtn;
    }

    public static IEnumerable<TypicalInlineParser> Load()
    {
        foreach (var info in TypicalParseInfo.Load(_resource)) yield return new TypicalInlineParser(info);
    }
}