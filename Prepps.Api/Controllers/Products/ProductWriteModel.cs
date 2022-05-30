using System.Text.Json.Serialization;

namespace Prepps.Api.Controllers.Products;

public class ProductWriteModel
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("expiresAt")]
    public string ExpiresAt { get; set; }
}