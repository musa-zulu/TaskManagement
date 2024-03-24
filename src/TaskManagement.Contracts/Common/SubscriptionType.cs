using System.Text.Json.Serialization;

namespace TaskManagement.Contracts.Common;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SubscriptionType
{
    Basic,
    Pro,
}