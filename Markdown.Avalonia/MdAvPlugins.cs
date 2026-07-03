using System.Linq;
using Markdown.Avalonia.Html;
using Markdown.Avalonia.Plugins;
using Markdown.Avalonia.Svg;
using Markdown.Avalonia.SyntaxHigh;

namespace Markdown.Avalonia.Full;

public class MdAvPlugins : Avalonia.MdAvPlugins
{
    protected override SetupInfo CreateInfo()
    {
        var setupInf = new SetupInfo();

        var hasSyntaxHigh = false;
        var hasSvgFormat = false;
        var hasHtml = false;

        SyntaxHighlight? syntaxPlugin = null;

        var (orderedPlugin, dic) = ComputeOrderedPlugins();

        foreach (var plugin in orderedPlugin)
        {
            if (plugin is IMdAvPluginRequestAnother another) another.Inject(another.DependsOn.Select(dep => dic[dep]));

            plugin.Setup(setupInf);

            if (plugin is SyntaxHighlight light)
            {
                hasSyntaxHigh = true;
                syntaxPlugin = light;
            }

            hasSvgFormat |= plugin is SvgFormat;
            hasHtml |= plugin is HtmlPlugin;
        }

        if (!hasSyntaxHigh)
        {
            syntaxPlugin = new SyntaxHighlight();
            syntaxPlugin.Setup(setupInf);
        }

        if (!hasSvgFormat)
        {
            var svgPlugin = new SvgFormat();
            svgPlugin.Setup(setupInf);
        }

        if (!hasHtml)
        {
            var htmlPlugin = new HtmlPlugin();
            htmlPlugin.Inject(new[] { syntaxPlugin });
            htmlPlugin.Setup(setupInf);
        }

        if (PathResolver is not null)
            setupInf.SetOnce(PathResolver);

        if (ContainerBlockHandler is not null)
            setupInf.SetOnce(ContainerBlockHandler);

        if (HyperlinkCommand is not null)
            setupInf.SetOnce(HyperlinkCommand);

        setupInf.Freeze();

        return setupInf;
    }
}