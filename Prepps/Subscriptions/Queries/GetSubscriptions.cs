using Google.Cloud.Firestore;

namespace Prepps.Subscriptions.Queries;

public interface IGetSubscriptions
{
    Task<IEnumerable<Subscription>> Execute();
}

public class GetSubscriptions : IGetSubscriptions
{
    private readonly FirestoreDb _db;

    public GetSubscriptions(FirestoreDb db)
    {
        _db = db;
    }

    public async Task<IEnumerable<Subscription>> Execute()
    {
        var collection = _db.Collection(nameof(Subscription));
        var snapshot = await collection.GetSnapshotAsync();

        var subs = snapshot.Documents.Select(x => x.ConvertTo<Subscription>()).ToList();
        return subs;
    }
}