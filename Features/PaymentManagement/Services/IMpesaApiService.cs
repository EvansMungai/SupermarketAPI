using Supermarket.API.Features.PaymentManagement.Models.Callback;
using Supermarket.API.Features.PaymentManagement.Models.LipaNaMpesa;
using Supermarket.API.Features.PaymentManagement.Models.OAuth;

namespace Supermarket.API.Features.PaymentManagement.Services;

public interface IMpesaApiService
{
    Task<OAuthResponseModel> GenerateAccessToken();
    Task<LipaNaMpesaResponseModel> LipaNaMpesa(string lipaNaMpesaUri, LipaNaMpesaRequestModel lipaNaMpesaModel);
    string GetValue(List<CallbackItem> items, string key);
}
