using MongoDB.Bson.Serialization.Attributes;

namespace Images;

public class CertifiedImage
{
    public static CertifiedImage Create(string content, string contentType)
    {
        return new CertifiedImage
        {
            Id = Guid.NewGuid(),
            Content = content,
            ContentType = contentType,
            SigningDate = DateTime.UtcNow
        };
    }

    public string ContentType { get; set; }

    [BsonId]
    public Guid Id { get; set; }
    public string Content { get; set; }
    public DateTime SigningDate { get; set; }
}