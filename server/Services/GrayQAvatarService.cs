using SkiaSharp;

namespace Gray.Server.Services;

internal class GrayQAvatarService(HttpClient client, ILogger<GrayQAvatarService> logger)
{
    private SKImage GetGrayAvatar(string id)
    {
        var url = $"https://q1.qlogo.cn/g?b=qq&nk={id}&s=640";
        using var stream = client.GetStreamAsync(url).Result;
        using var input = SKImage.FromEncodedData(stream);
        return input.ToGray();
    }
    private static WeakReference<SKImage> Frame = new(null!);
    private static WeakReference<SKImage> Smoke = new(null!);
    public IResult Generate(string id, int? quality)
    {
        logger.LogInformation("Generate avatar for {id}", id);
        using var image = GetGrayAvatar(id);
        using var surface = SKSurface.Create(new SKImageInfo(867, 867, SKColorType.Rgba8888));
        using var canvas = surface.Canvas;
        canvas.DrawImage(image, new SKRect(120, 83, 800, 763));
        if (!Smoke.TryGetTarget(out var smoke))
        {
            smoke = SKImage.FromEncodedData("Resources/smoke.png");
        }
        canvas.DrawImage(smoke, new SKPoint(0, 0));
        if (!Frame.TryGetTarget(out var frame))
        {
            frame = SKImage.FromEncodedData("Resources/frame.png");
        }
        canvas.DrawImage(frame, new SKPoint(0, 0));

        using var output = surface.Snapshot();
        var data = output.Encode(SKEncodedImageFormat.Webp, quality ?? 100);
        return Results.File(data.AsStream(), "image/webp");
    }
}
