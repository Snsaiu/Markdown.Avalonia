using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UnitTest.CTxt.Xamls;

public partial class Test3 : UserControl
{
    public Test3()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}