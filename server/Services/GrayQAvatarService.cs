using SkiaSharp;

namespace Gray.Server.Services;

internal class GrayQAvatarService(HttpClient client, ILogger<GrayQAvatarService> logger)
{
    public IResult Generate(string id, int? quality)
    {
        logger.LogInformation("Generate avatar for {id}", id);
        var url = $"https://q1.qlogo.cn/g?b=qq&nk={id}&s=640";
        using var stream = client.GetStreamAsync(url).Result;
        using var input = SKImage.FromEncodedData(stream);
        using var output = input.ToGray();
        var data = output.Encode(SKEncodedImageFormat.Webp, quality ?? 100);
        return Results.File(data.AsStream(), "image/webp");
    }
}
