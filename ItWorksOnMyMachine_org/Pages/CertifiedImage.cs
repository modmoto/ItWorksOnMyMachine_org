using MongoDB.Bson.Serialization.Attributes;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace ItWorksOnMyMachine_org.Pages;

public class CertifiedImage
{
    public static async Task<CertifiedImage> Create(Stream content)
    {
        var image = await Image.LoadAsync(content);
        var imgNew = image.Clone(processingContext =>
        {
            var waterMark = Image.Load("Pages/watermark.png");
            processingContext.DrawImage(waterMark, 0.8f);
        });

        var base64String = imgNew.ToBase64String(PngFormat.Instance);
        return new CertifiedImage
        {
            Id = Guid.NewGuid(),
            Content = base64String,
            SigningDate = DateTime.UtcNow
        };
    }
    [BsonId]
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime SigningDate { get; set; }
}