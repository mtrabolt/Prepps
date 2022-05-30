using Marten;

namespace Prepps.Subscriptions.Commands;

public interface IDeleteSubscription
{
    Task Execute(Guid id);
}

public class DeleteSubscription : IDeleteSubscription
{
    private readonly IDocumentSession _session;

    public DeleteSubscription(IDocumentSession session)
    {
        _session = session;
    }
    
    public async Task Execute(Guid id)
    {
        _session.Delete<Subscription>(id);
        await _session.SaveChangesAsync();
    }
}