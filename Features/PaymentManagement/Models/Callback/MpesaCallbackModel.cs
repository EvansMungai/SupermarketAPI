using System.Text.Json.Serialization;

namespace Supermarket.API.Features.PaymentManagement.Models.Callback;

public class MpesaCallbackModel
{
    [JsonPropertyName("Body")]
    public CallbackBody Body { get; set; }
}
