using System.Text.Json.Serialization;

namespace Supermarket.API.Features.PaymentManagement.Models.Callback;

public class CallbackMetadata
{
    [JsonPropertyName("Item")]
    public List<CallbackItem> Item { get; set; }
}
