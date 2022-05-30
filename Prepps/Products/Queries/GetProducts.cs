using Marten;
using Marten.Linq;

namespace Prepps.Products.Queries;

public interface IGetProducts
{
    IEnumerable<Product> Execute();
}

public class GetProducts : IGetProducts
{
    private readonly IDocumentSession _session;

    public GetProducts(IDocumentSession session)
    {
        _session = session;
    }
    
    public IEnumerable<Product> Execute()
    {
        return _session.Query<Product>();
    }
}