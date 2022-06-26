using Google.Cloud.Firestore;

namespace Prepps.Subscriptions.Commands;

public interface IStoreSubscription
{
    Task Execute(Subscription subscription);
}

public class StoreSubscription : IStoreSubscription
{
    private readonly FirestoreDb _db;

    public StoreSubscription(FirestoreDb db)
    {
        _db = db;
    }

    public async Task Execute(Subscription subscription)
    {
        var document = _db.Collection(nameof(Subscription)).Document(subscription.Id);
        await document.SetAsync(subscription);
    }
}