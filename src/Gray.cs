using SkiaSharp;

namespace Gray;

public static class SKImageExtensions
{
    public static SKImage ToGray(this SKImage image)
    {
        var info = image.Info;
        var surface = SKSurface.Create(new SKImageInfo(info.Width, info.Height, SKColorType.Gray8));
        var canvas = surface.Canvas;
        canvas.Clear(SKColors.White);
        canvas.DrawImage(image, 0, 0);
        return surface.Snapshot();
    }
}
