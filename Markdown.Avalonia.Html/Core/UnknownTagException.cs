using System;
using HtmlAgilityPack;

namespace Markdown.Avalonia.Html.Core;

/// <summary>
///     MarkdownFromHtml can not convert a certain tag.
///     This exception is thrown when <see cref="UnknownTagsOption.Raise" /> is set to
///     <see cref="ReplaceManager.UnknownTags" />.
/// </summary>
public class UnknownTagException : Exception
{
    public UnknownTagException(HtmlNode node) : base($"unknown tag: {node.Name}")
    {
        TagName = node.Name;
        Content = node.OuterHtml;
    }

    /// <summary>
    ///     Tag name that could not be converted
    /// </summary>
    public string TagName { get; }

    /// <summary>
    ///     Tag that could not be converted
    /// </summary>
    public string Content { get; }
}