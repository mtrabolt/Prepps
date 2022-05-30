using Marten;

namespace Prepps.Products.Commands;

public interface IStoreProduct
{
    Task Execute(Product product);
}

public class StoreProduct : IStoreProduct
{
    private readonly IDocumentSession _session;

    public StoreProduct(IDocumentSession session)
    {
        _session = session;
    }
    
    public async Task Execute(Product product)
    {
        _session.Store(product);
        await _session.SaveChangesAsync();
    }
}