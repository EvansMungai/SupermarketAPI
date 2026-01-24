using System.Runtime.Serialization;

namespace Supermarket.API.Features.PaymentManagement.Models.LipaNaMpesa;

[DataContract]
public class LipaNaMpesaRequestModel
{
    [DataMember(Name = "BusinessShortCode")]
    public int BusinessShortCode { get; set; }

    [DataMember(Name = "Password")]
    public string Password { get; set; }

    [DataMember(Name = "Timestamp")]
    public string Timestamp { get; set; }

    [DataMember(Name = "TransactionType")]
    public string TransactionType { get; set; }

    [DataMember(Name = "Amount")]
    public decimal Amount { get; set; }

    [DataMember(Name = "PartyA")]
    public string PartyA { get; set; }

    [DataMember(Name = "PartyB")]
    public string PartyB { get; set; }

    [DataMember(Name = "PhoneNumber")]
    public string PhoneNumber { get; set; }

    [DataMember(Name = "CallBackURL")]
    public string CallBackUrl { get; set; }

    [DataMember(Name = "AccountReference")]
    public string AccountReference { get; set; }

    [DataMember(Name = "TransactionDesc")]
    public string TransactionDescription { get; set; }
}
