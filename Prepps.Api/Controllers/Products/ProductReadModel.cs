using System.Text.Json.Serialization;
using Prepps.Extensions;
using Prepps.Products;

namespace Prepps.Api.Controllers.Products;

public class ProductReadModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly ExpiresAt { get; set; }
    public bool IsExpired { get; set; }

    public static ProductReadModel FromProduct(Product product)
    {
        return new()
        {
            Id = product.Id,
            Name = product.Name,
            ExpiresAt = product.ExpiresAt,
            IsExpired = product.ExpiresAt.ToDateTime(TimeOnly.MinValue) < DateTime.UtcNow,
        };
    }
}