namespace Markdown.Avalonia.Full;

public class MarkdownScrollViewer : Avalonia.MarkdownScrollViewer
{
    public MarkdownScrollViewer()
    {
        Plugins = new MdAvPlugins();
    }
}