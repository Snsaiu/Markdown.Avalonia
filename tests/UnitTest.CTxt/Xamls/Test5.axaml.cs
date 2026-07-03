using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UnitTest.CTxt.Xamls;

public partial class Test5 : UserControl
{
    public Test5()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}