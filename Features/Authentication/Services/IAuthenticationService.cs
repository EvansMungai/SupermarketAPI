using Supermarket.API.Features.Authentication.Models;

namespace Supermarket.API.Features.Authentication.Services;

public interface IAuthenticationService
{
    Task<User> CreateUserAsync(string username, string password, string role);
    Task<object?> LoginAsync(string username, string password);
    Task LogoutAsync(); // for cookie auth
    Task<User?> GetUserByUsernameAsync(string username);
}
