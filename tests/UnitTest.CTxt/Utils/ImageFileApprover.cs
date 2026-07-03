using System.IO;
using ApprovalTests.Approvers;
using ApprovalTests.Core;
using ApprovalTests.Core.Exceptions;
using SkiaSharp;

namespace UnitTest.CTxt.Utils;

public class ImageFileApprover : FileApprover
{
    public ImageFileApprover(IApprovalWriter writer, IApprovalNamer namer, bool normalizeLineEndingsForTextFiles = false)
        : base(writer, namer, normalizeLineEndingsForTextFiles)
    {
    }

    public override ApprovalException Approve(string approvedPath, string receivedPath)
    {
        if (Path.GetExtension(approvedPath) != ".png")
            return base.Approve(approvedPath, receivedPath);

        if (!File.Exists(approvedPath)) return new ApprovalMissingException(receivedPath, approvedPath);

        using var approvedImg = SKBitmap.Decode(approvedPath);
        using var receivedImg = SKBitmap.Decode(receivedPath);

        return !ImagesEqual(receivedImg, approvedImg) ? new ApprovalMismatchException(receivedPath, approvedPath) : null;
    }

    private static bool ImagesEqual(SKBitmap? image1, SKBitmap? image2)
    {
        if (image1 is null || image2 is null ||
            image1.Width != image2.Width || image1.Height != image2.Height)
            return false;

        for (var y = 0; y < image1.Height; y++)
        for (var x = 0; x < image1.Width; x++)
        {
            var pixel1 = image1.GetPixel(x, y);
            var pixel2 = image2.GetPixel(x, y);
            if (pixel1.Red != pixel2.Red ||
                pixel1.Green != pixel2.Green ||
                pixel1.Blue != pixel2.Blue)
                return false;
        }

        return true;
    }
}