using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using HtmlAgilityPack;

namespace Markdown.Avalonia.Html.Core.Parsers;

public class TypicalBlockParser : IBlockTagParser
{
    private const string _resource = "Markdown.Avalonia.Html.Core.Parsers.TypicalBlockParser.tsv";
    private readonly TypicalParseInfo _parser;

    public TypicalBlockParser(TypicalParseInfo parser)
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

    public bool TryReplace(HtmlNode node, ReplaceManager manager, out IEnumerable<Control> generated)
    {
        var rtn = _parser.TryReplace(node, manager, out var list);
        generated = list.Cast<Control>();
        return rtn;
    }

    public static IEnumerable<TypicalBlockParser> Load()
    {
        foreach (var info in TypicalParseInfo.Load(_resource)) yield return new TypicalBlockParser(info);
    }
}