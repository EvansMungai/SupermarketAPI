using System.Runtime.Serialization;

namespace Supermarket.API.Features.PaymentManagement.Models.LipaNaMpesa;

[DataContract]
public class LipaNaMpesaResponseModel
{
    [DataMember(Name = "MerchantRequestID")]
    public string MerchantRequestID { get; set; }

    [DataMember(Name = "CheckoutRequestID")]
    public string CheckoutRequestID { get; set; }

    [DataMember(Name = "ResponseCode")]
    public int ResponseCode { get; set; }

    [DataMember(Name = "ResponseDescription")]
    public string ResponseDescription { get; set; }

    [DataMember(Name = "CustomerMessage")]
    public string CustomerMessage { get; set; }

    [DataMember(Name = "MpesaReceiptNumber")]
    public string MpesaReceiptNumber { get; set; }
}
