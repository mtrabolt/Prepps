using Google.Cloud.Firestore;

namespace Prepps.Subscriptions;

[FirestoreData]
public class Subscription
{
    [FirestoreProperty]
    public string Id { get; set; }
    
    [FirestoreProperty]
    public string Email { get; set; }
}