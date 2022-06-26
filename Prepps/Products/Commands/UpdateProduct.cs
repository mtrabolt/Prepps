using Google.Cloud.Firestore;

namespace Prepps.Products.Commands;

public interface IUpdateProduct
{
    Task Execute(Guid id, string name, DateOnly expiresAt);
}

public class UpdateProduct : IUpdateProduct
{
    private readonly FirestoreDb _db;

    private readonly IStoreProduct _storeProduct;

    public UpdateProduct(FirestoreDb db,
        IStoreProduct storeProduct)
    {
        _db = db;
        _storeProduct = storeProduct;
    }
    
    public async Task Execute(Guid id, string name, DateOnly expiresAt)
    {
        var document = _db.Collection(nameof(Product)).Document(id.ToString());
        var snapshot = await document.GetSnapshotAsync();
        var product = snapshot.ConvertTo<Product>();

        product.Name = name;
        product.ExpiresAt = expiresAt;

        await _storeProduct.Execute(product);
    }
}