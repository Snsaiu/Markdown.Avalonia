using System.Linq;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Markdown.Avalonia;

namespace Markdown.AvaloniaDemo.Views;

public partial class MainWindow : Window
{
    private readonly Label _breadcrumb;

    public MainWindow()
    {
        InitializeComponent();

        _breadcrumb = this.FindControl<Label>("Breadcrumb");
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void HeaderScrolled(object sender, HeaderScrolledEventArgs args)
    {
        _breadcrumb.Content = string.Join(" > ", args.Tree.Select(tag => tag.Text));
    }
}