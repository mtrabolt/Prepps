using Marten;

namespace Prepps.Subscriptions.Queries;

public interface IGetSubscriptions
{
    IEnumerable<Subscription> Execute();
}

public class GetSubscriptions : IGetSubscriptions
{
    private readonly IDocumentSession _session;

    public GetSubscriptions(IDocumentSession session)
    {
        _session = session;
    }
    
    public IEnumerable<Subscription> Execute()
    {
        return _session.Query<Subscription>();
    }
}