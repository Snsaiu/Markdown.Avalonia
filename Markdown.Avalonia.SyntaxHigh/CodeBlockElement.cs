using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Layout;
using AvaloniaEdit;
using ColorDocument.Avalonia;
using Markdown.Avalonia.SyntaxHigh.Extensions;

namespace Markdown.Avalonia.SyntaxHigh;

internal class CodeBlockElement : DocumentElement
{
    private readonly string _code;
    private readonly Lazy<Border> _control;
    private readonly SyntaxHighlightProvider _provider;

    public CodeBlockElement(SyntaxHighlightProvider provider, string lang, string code)
    {
        _provider = provider;
        _code = code;
        _control = new Lazy<Border>(() => Create(lang, code));
    }

    public override Control Control => _control.Value;

    public override IEnumerable<DocumentElement> Children => Array.Empty<DocumentElement>();

    public override void ConstructSelectedText(StringBuilder stringBuilder)
    {
        stringBuilder.Append(_code);
    }

    public override void Select(Point from, Point to)
    {
        Helper?.Register(Control);
    }

    public override void UnSelect()
    {
        Helper?.Unregister(Control);
    }

    private Border Create(string lang, string code)
    {
        var langLabel = new Label { Content = lang };
        langLabel.Classes.Add("LangInfo");

        var copyButton = new Button { Content = new TextBlock() };
        copyButton.Classes.Add("CopyButton");

        var txtEdit = new TextEditor();
        txtEdit.Tag = lang;
        txtEdit.SetValue(SyntaxHighlightWrapperExtension.ProviderProperty, _provider);
        txtEdit.Text = code;
        txtEdit.HorizontalAlignment = HorizontalAlignment.Stretch;
        txtEdit.IsReadOnly = true;
        txtEdit.AddHandler(TextEditor.RequestBringIntoViewEvent, (_, e) => { e.Handled = true; });

        copyButton.Click += (s, e) =>
        {
            var clipboard = TopLevel.GetTopLevel(txtEdit)?.Clipboard;
            clipboard?.SetTextAsync(txtEdit.Text);
        };

        var cdPd = new CodePad();
        cdPd.Content = txtEdit;
        cdPd.ExandableMenu = copyButton;
        cdPd.AlwaysShowMenu = langLabel;

        var result = new Border();
        result.Classes.Add(Markdown.CodeBlockClass);
        result.Child = cdPd;

        return result;
    }
}