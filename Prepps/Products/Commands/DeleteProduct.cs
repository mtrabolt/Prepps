using Google.Cloud.Firestore;

namespace Prepps.Products.Commands;

public interface IDeleteProduct
{
    Task Execute(Guid id);
}

public class DeleteProduct : IDeleteProduct
{
    private readonly FirestoreDb _db;

    public DeleteProduct(FirestoreDb db)
    {
        _db = db;
    }

    public async Task Execute(Guid id)
    {
        var document = _db.Collection(nameof(Product)).Document(id.ToString());
        await document.DeleteAsync();
    }
}