using Marten;

namespace Prepps.Subscriptions.Commands;

public interface IStoreSubscription
{
    Task Execute(Subscription subscription);
}

public class StoreSubscription : IStoreSubscription
{
    private readonly IDocumentSession _session;

    public StoreSubscription(IDocumentSession session)
    {
        _session = session;
    }
    
    public async Task Execute(Subscription subscription)
    {
        _session.Store(subscription);
        await _session.SaveChangesAsync();
    }
}