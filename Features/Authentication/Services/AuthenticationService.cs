using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supermarket.API.Data.Infrastructure;
using Supermarket.API.Features.Authentication.Models;
using System;
using System.Security.Claims;

namespace Supermarket.API.Features.Authentication.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly SupermarketContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly PasswordHasher<User> _hasher;

    public AuthenticationService(SupermarketContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _hasher = new PasswordHasher<User>();
    }

    public async Task<User> CreateUserAsync(string username, string password, string role)
    {
        if (await _context.Users.AnyAsync(u => u.UserName == username))
            throw new Exception("User already exists");

        User user = new User
        {
            UserName = username,
            Role = role
        };

        user.PasswordHash = _hasher.HashPassword(user, password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<object?> LoginAsync(string username, string password)
    {
        var user = await GetUserByUsernameAsync(username);
        if (user == null) return null;

        var result = _hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Failed)
            return null;

        // ✅ Cookie authentication
        var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        await _httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

        return new
        {
            username = user.UserName,
            role = user.Role
        };
    }

    public async Task LogoutAsync()
    {
        await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
