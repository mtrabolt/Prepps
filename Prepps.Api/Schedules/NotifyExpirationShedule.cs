// using Prepps.Infrastructure.CronServices;
using Prepps.Products.Queries;
using Prepps.Subscriptions.Queries;

namespace Prepps.Api.Schedules;

// TODO: Make schedule work
// public class NotifyExpirationShedule : CronJobService
// {
//     private readonly IGetProducts _getProducts;
//     private readonly IGetSubscriptions _getSubscriptions;
//
//     private const string _cronExpression = "";
//
//     protected NotifyExpirationShedule(IGetProducts getProducts, IGetSubscriptions getSubscriptions) 
//         : base(_cronExpression, TimeZoneInfo.Local)
//     {
//         _getProducts = getProducts;
//         _getSubscriptions = getSubscriptions;
//     }
//
//     public override Task Execute(CancellationToken cancellationToken)
//     {
//         var today = DateOnly.FromDateTime(DateTime.UtcNow);
//         
//         var productItems = _getProducts.Execute().ToList();
//         var subscriptions = _getSubscriptions.Execute();
//
//         var expiredItems = productItems.Where(x => x.ExpiresAt < today);
//
//         if (expiredItems.Any())
//         {
//             // _sendEmailToSubscribers.Execute(expiredItems, subscriptions);
//         }
//         
//         return Task.CompletedTask;
//     }
// }