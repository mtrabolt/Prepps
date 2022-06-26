using Google.Cloud.Firestore;

namespace Prepps.Products.Queries;

public interface IGetProducts
{
    Task<IEnumerable<Product>> Execute();
}

public class GetProducts : IGetProducts
{
    private readonly FirestoreDb _db;
    
    public GetProducts(FirestoreDb db)
    {
        _db = db;
    }
    
    public async Task<IEnumerable<Product>> Execute()
    {
        var collection = _db.Collection(nameof(Product));
        var snapshot = await collection.GetSnapshotAsync();
        return snapshot.Documents.Select(x => x.ConvertTo<Product>());
    }
}