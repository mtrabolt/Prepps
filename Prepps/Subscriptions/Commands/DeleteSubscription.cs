using Google.Cloud.Firestore;

namespace Prepps.Subscriptions.Commands;

public interface IDeleteSubscription
{
    Task Execute(Guid id);
}

public class DeleteSubscription : IDeleteSubscription
{
    private readonly FirestoreDb _db;

    public DeleteSubscription(FirestoreDb db)
    {
        _db = db;
    }

    public async Task Execute(Guid id)
    {
        var document = _db.Collection(nameof(Subscription)).Document(id.ToString());
        await document.DeleteAsync();
    }
}