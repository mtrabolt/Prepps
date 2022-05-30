using Marten;

namespace Prepps.Products.Commands;

public interface IDeleteProduct
{
    Task Execute(Guid id);
}

public class DeleteProduct : IDeleteProduct
{
    private readonly IDocumentSession _session;

    public DeleteProduct(IDocumentSession session)
    {
        _session = session;
    }
    
    public async Task Execute(Guid id)
    {
        _session.Delete<Product>(id);
        await _session.SaveChangesAsync();
    }
}