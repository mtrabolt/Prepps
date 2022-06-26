using Google.Cloud.Firestore;

namespace Prepps.Products.Commands;

public interface IStoreProduct
{
    Task Execute(Product product);
}

public class StoreProduct : IStoreProduct
{
    private readonly FirestoreDb _db;

    public StoreProduct(FirestoreDb db)
    {
        _db = db;
    }
    
    public async Task Execute(Product product)
    {
        var document = _db.Collection(nameof(Product)).Document(product.Id.ToString());
        await document.SetAsync(product);
    }
}