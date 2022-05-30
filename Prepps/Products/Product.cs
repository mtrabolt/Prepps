using System.Text.Json.Serialization;
using Prepps.Extensions;

namespace Prepps.Products;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly ExpiresAt { get; set; }
}