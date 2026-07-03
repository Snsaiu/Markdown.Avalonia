using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UnitTest.CTxt.Xamls;

public partial class Test1 : UserControl
{
    public Test1()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}