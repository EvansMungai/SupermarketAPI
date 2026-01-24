using System.Runtime.Serialization;

namespace Supermarket.API.Features.PaymentManagement.Models.OAuth;

[DataContract]
public class OAuthResponseModel
{
    [DataMember(Name = "access_token")]
    public string AccessToken { get; set; }

    [DataMember(Name = "expires_in")]
    public string ExpiresIn { get; set; }
}
