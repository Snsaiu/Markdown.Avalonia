using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CustomStyle
{
    public partial class SetStyles : UserControl
    {
        public SetStyles()
        {
            this.InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
