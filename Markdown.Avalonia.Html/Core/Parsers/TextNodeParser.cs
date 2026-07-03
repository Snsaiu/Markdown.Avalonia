using System.Collections.Generic;
using Avalonia;
using ColorTextBlock.Avalonia;
using HtmlAgilityPack;
using Markdown.Avalonia.Html.Core.Utils;

namespace Markdown.Avalonia.Html.Core.Parsers;

public class TextNodeParser : IInlineTagParser
{
    public IEnumerable<string> SupportTag => new[] { HtmlNode.HtmlNodeTypeNameText };

    bool ITagParser.TryReplace(HtmlNode node, ReplaceManager manager, out IEnumerable<StyledElement> generated)
    {
        var rtn = TryReplace(node, manager, out var list);
        generated = list;
        return rtn;
    }

    public bool TryReplace(HtmlNode node, ReplaceManager manager, out IEnumerable<CInline> generated)
    {
        if (node is HtmlTextNode textNode)
        {
            generated = Replace(textNode.Text, manager);
            return true;
        }

        generated = EnumerableExt.Empty<CInline>();
        return false;
    }

    public IEnumerable<CInline> Replace(string text, ReplaceManager manager)
    {
        return text.StartsWith("\n") ? new[] { new CRun { Text = text.Replace('\n', ' ') } } : manager.Engine.RunSpanGamut(text.Replace('\n', ' '));
    }
}