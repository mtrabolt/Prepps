using Marten;

namespace Prepps.Products.Commands;

public interface IUpdateProduct
{
    Task Execute(Guid id, string name, DateOnly expiresAt);
}

public class UpdateProduct : IUpdateProduct
{
    private readonly IDocumentSession _session;

    public UpdateProduct(IDocumentSession session)
    {
        _session = session;
    }
    
    public async Task Execute(Guid id, string name, DateOnly expiresAt)
    {
        var product = await _session.LoadAsync<Product>(id);

        if (product == null)
        {
            throw new Exception("productNotFound");
        }
        
        product.Name = name;
        product.ExpiresAt = expiresAt;

        _session.Store(product);
        await _session.SaveChangesAsync();
    }
}