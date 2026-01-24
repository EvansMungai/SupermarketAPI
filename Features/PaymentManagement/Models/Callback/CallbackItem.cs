using System.Text.Json.Serialization;

namespace Supermarket.API.Features.PaymentManagement.Models.Callback;

public class CallbackItem
{
    [JsonPropertyName("Name")]
    public string Name { get; set; }

    [JsonPropertyName("Value")]
    public object Value { get; set; }
}
