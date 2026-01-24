using System.Runtime.Serialization;

namespace Supermarket.API.Features.PaymentManagement.Models.Common;

[DataContract]
public class CommonResponseModel
{
    [DataMember(Name = "OriginatorConversationID")]
    public string OriginatorConversationId { get; set; }

    [DataMember(Name = "ConversationID")]
    public string ConversationId { get; set; }

    [DataMember(Name = "ResponseDescription")]
    public string ResponseDescription { get; set; }
}
