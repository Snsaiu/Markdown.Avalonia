using System;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using Avalonia.Media;
using Avalonia.Svg;
using Markdown.Avalonia.Utils;

namespace Markdown.Avalonia.Svg;

internal class SvgImageResolver : IImageResolver
{
    public async Task<IImage?> Load(Stream stream)
    {
        var task = Task.Run(() =>
        {
            if (IsSvgFile(stream)) return (IImage)new SvgImage { Source = SvgSource.Load(stream) };

            return null;
        });

        return await task;
    }

    private static bool IsSvgFile(Stream fileStream)
    {
        try
        {
            var firstChr = fileStream.ReadByte();
            if (firstChr != ('<' & 0xFF))
                return false;

            fileStream.Seek(0, SeekOrigin.Begin);
            using (var xmlReader = XmlReader.Create(fileStream))
            {
                return xmlReader.MoveToContent() == XmlNodeType.Element &&
                       "svg".Equals(xmlReader.Name, StringComparison.OrdinalIgnoreCase);
            }
        }
        catch
        {
            return false;
        }
        finally
        {
            fileStream.Seek(0, SeekOrigin.Begin);
        }
    }
}