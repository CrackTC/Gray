using SkiaSharp;

namespace Gray.Test;

class Program
{
    static void Main()
    {
        Console.Write("path: ");
        string path = Console.ReadLine() ?? throw new ArgumentNullException(nameof(path));
        var fileName = Path.GetFileName(path);
        var image = SKImage.FromEncodedData(path);
        var gray = image.ToGray();
        gray.Encode(SKEncodedImageFormat.Jpeg, 80).SaveTo(File.OpenWrite(fileName + "_gray.jpg"));
    }
}
