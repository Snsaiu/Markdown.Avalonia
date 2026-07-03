using System;
using System.Collections.Generic;
using System.IO;
using Avalonia.Platform;
using Markdown.Avalonia;
using ReactiveUI;

namespace Markdown.AvaloniaDemo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _appendStyleXamlText;

    private string _edittingStyleXamlText;

    private string _ErrorInfo;

    private StyleViewModel _selectedStyle;
    private string _text;

    public MainWindowViewModel()
    {
        try
        {
            using (var stream = new FileStream("MainWindow.md", FileMode.Open))
            using (var reader = new StreamReader(stream))
            {
                Text = reader.ReadToEnd();
            }
        }
        catch
        {
        }

        Styles = new List<StyleViewModel>
        {
            new() { Name = nameof(MarkdownStyle.Standard) },
            new() { Name = nameof(MarkdownStyle.SimpleTheme) },
            new() { Name = nameof(MarkdownStyle.GithubLike) }
        };

        SelectedStyle = Styles[1];

        using (var strm = AssetLoader.Open(new Uri("avares://Markdown.AvaloniaDemo/Assets/XamlTemplate.txt")))
        using (var reader = new StreamReader(strm))
        {
            EdittingStyleXamlText = reader.ReadToEnd();
        }
    }

    public string Text
    {
        get => _text;
        set => this.RaiseAndSetIfChanged(ref _text, value);
    }

    public string EdittingStyleXamlText
    {
        get => _edittingStyleXamlText;
        set => this.RaiseAndSetIfChanged(ref _edittingStyleXamlText, value);
    }

    public string AppendStyleXamlText
    {
        get => _appendStyleXamlText;
        set => this.RaiseAndSetIfChanged(ref _appendStyleXamlText, value);
    }

    public StyleViewModel SelectedStyle
    {
        get => _selectedStyle;
        set => this.RaiseAndSetIfChanged(ref _selectedStyle, value);
    }

    public string ErrorInfo
    {
        get => _ErrorInfo;
        set => this.RaiseAndSetIfChanged(ref _ErrorInfo, value);
    }

    public List<StyleViewModel> Styles { get; set; }

    public void XamlParseResult(string result)
    {
        ErrorInfo = result;
    }

    public void TryParse()
    {
        AppendStyleXamlText = EdittingStyleXamlText;
    }
}

public class StyleViewModel
{
    public string Name { get; set; }
}