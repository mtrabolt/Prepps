using System.Text.Json;
using Prepps.Api.Controllers.Products;
using Prepps.Products;
using Xunit;

namespace Prepps.Tests;

public class JsonConverterTests
{
    private DateTime _date = DateTime.UtcNow;
    
    [Fact]
    public void Test()
    {
        var product = new Product
        {
            Name = "Test",
            ExpiresAt = DateOnly.FromDateTime(_date),
        };

        var s = JsonSerializer.Serialize(product);

        var result = JsonSerializer.Deserialize<Product>(s);
    }
}