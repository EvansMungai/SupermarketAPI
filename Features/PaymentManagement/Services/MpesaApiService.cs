using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Supermarket.API.Features.PaymentManagement.Models.Callback;
using Supermarket.API.Features.PaymentManagement.Models.Common;
using Supermarket.API.Features.PaymentManagement.Models.LipaNaMpesa;
using Supermarket.API.Features.PaymentManagement.Models.OAuth;
using System.Text;

namespace Supermarket.API.Features.PaymentManagement.Services;

public class MpesaApiService : IMpesaApiService
{
    private readonly HttpClient _httpClient;
    private readonly MpesaConfig _mpesaConfig;

    public MpesaApiService(HttpClient httpClient, IOptions<MpesaConfig> mpesaConfig)
    {
        _httpClient = httpClient;
        _mpesaConfig = mpesaConfig.Value;
    }

    public async Task<OAuthResponseModel> GenerateAccessToken()
    {
        string encodedKeySecret = GenerateEncodedKeySecret();
        string oAuthUri = "https://sandbox.safaricom.co.ke/oauth/v1/generate?grant_type=client_credentials";
        //Remove initial authorization header
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
        //set Authorization header 
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + encodedKeySecret);
        //send request            
        var response = await _httpClient.GetAsync(oAuthUri);
        //ensure success
        //response.EnsureSuccessStatusCode();
        //stringify content
        var resultContent = await response.Content.ReadAsStringAsync();
        //deserialize object
        var result = JsonConvert.DeserializeObject<OAuthResponseModel>(resultContent);

        return result;
    }

    private async Task<T> GetMpesaResponseAsync<T, U>(string requestUri, U requestModel)
    {
        //Get the access token
        var accessTokenModel = await GenerateAccessToken();
        //serialize request model
        var serializedObject = JsonConvert.SerializeObject(requestModel);
        //set the content
        var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
        //Remove initial authorization header
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
        //Set current authorization header
        _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessTokenModel.AccessToken);
        //send request
        var response = await _httpClient.PostAsync(requestUri, content);

        // Log status code
        Console.WriteLine($"Status Code: {response.StatusCode}"); 
        // Read the raw response body
        var responseBody = await response.Content.ReadAsStringAsync();
        //Console.WriteLine($"Response Body: {responseBody}");
        //ensure success
        response.EnsureSuccessStatusCode();
        //stringify content
        var resultContent = await response.Content.ReadAsStringAsync();
        //deserialize object
        var result = JsonConvert.DeserializeObject<T>(resultContent);

        return result;
    }

    public async Task<LipaNaMpesaResponseModel> LipaNaMpesa(string lipaNaMpesaUri, LipaNaMpesaRequestModel lipaNaMpesaModel)
    {
        var result = await GetMpesaResponseAsync<LipaNaMpesaResponseModel, LipaNaMpesaRequestModel>(lipaNaMpesaUri, lipaNaMpesaModel);

        return result;
    }

    #region Helpers 
    public string GenerateEncodedKeySecret()
    {
        string consumerKey = _mpesaConfig.ConsumerKey.Trim();
        string consumerSecret = _mpesaConfig.ConsumerSecret.Trim();

        if (string.IsNullOrEmpty(consumerKey) || string.IsNullOrEmpty(consumerSecret))
        {
            throw new Exception("ConsumerKey or ConsumerSecret is missing in appsettings.json.");
        }

        string keySecret = $"{consumerKey}:{consumerSecret}";
        byte[] keySecretBytes = Encoding.UTF8.GetBytes(keySecret);
        return Convert.ToBase64String(keySecretBytes);
    }

    public string GetValue(List<CallbackItem> items, string key)
    {
        return items?.FirstOrDefault(i => i.Name == key)?.Value?.ToString();
    }
    #endregion
}
