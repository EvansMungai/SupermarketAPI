using System.Text;

namespace Supermarket.API.Features.PaymentManagement.Models.OAuth;

public class OAuthModel
{
    public string ConsumerKey { get; set; }
    public string ConsumerSecret { get; set; }
    public string EncodedKeySecret
    {
        get
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(this.ConsumerKey + ":" + this.ConsumerSecret));
        }
    }
}
