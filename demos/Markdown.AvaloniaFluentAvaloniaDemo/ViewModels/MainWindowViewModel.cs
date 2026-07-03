using System;
using System.IO;
using Avalonia.Platform;
using ReactiveUI;

namespace Markdown.AvaloniaFluentAvaloniaDemo.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private string _appendStyleXamlText;

    private string _AssetPathRoot;

    private string _AssetPathRootText;

    private string _edittingStyleXamlText;

    private string _ErrorInfo;

    private Uri _Source;

    private string _SourceText;
    private string _text;

    public MainWindowViewModel()
    {
        using (var stream = new FileStream("MainWindow.md", FileMode.Open))
        using (var reader = new StreamReader(stream))
        {
            Text = reader.ReadToEnd();
        }

        using (var strm = AssetLoader.Open(new Uri("avares://Markdown.AvaloniaFluentAvaloniaDemo/Assets/XamlTemplate.txt")))
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

    public string ErrorInfo
    {
        get => _ErrorInfo;
        set => this.RaiseAndSetIfChanged(ref _ErrorInfo, value);
    }

    public string AssetPathRootText
    {
        get => _AssetPathRootText;
        set => this.RaiseAndSetIfChanged(ref _AssetPathRootText, value);
    }

    public string SourceText
    {
        get => _SourceText;
        set => this.RaiseAndSetIfChanged(ref _SourceText, value);
    }

    public string AssetPathRoot
    {
        get => _AssetPathRoot;
        set
        {
            _AssetPathRoot = value;
            this.RaisePropertyChanged();
        }
    }

    public Uri Source
    {
        get => _Source;
        set
        {
            _Source = value;
            this.RaisePropertyChanged();
        }
    }

    public void XamlParseResult(string result)
    {
        ErrorInfo = result;
    }

    public void TryParse()
    {
        AppendStyleXamlText = EdittingStyleXamlText;
    }

    public void ApplyAssetPathRoot()
    {
        AssetPathRoot = AssetPathRootText;
    }

    public void ApplySource()
    {
        Source = new Uri(SourceText);
    }
}