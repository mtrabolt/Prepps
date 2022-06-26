using System.Text.Json.Serialization;
using Google.Cloud.Firestore;
using Prepps.Extensions;

namespace Prepps.Products;

[FirestoreData]
public class Product
{
    [FirestoreProperty]
    public string Id { get; set; }
    
    [FirestoreProperty]
    public string Name { get; set; }
    
    [FirestoreProperty(ConverterType = typeof(DateOnlyConverter))]
    [JsonConverter(typeof(DateOnlyConverter))]
    public DateOnly ExpiresAt { get; set; }
}