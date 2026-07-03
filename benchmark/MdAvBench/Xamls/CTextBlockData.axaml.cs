using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UnitTest.CTxt.Xamls;

public class CTextBlockData : UserControl
{
    public CTextBlockData()
    {
        InitializeComponent();
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }
}