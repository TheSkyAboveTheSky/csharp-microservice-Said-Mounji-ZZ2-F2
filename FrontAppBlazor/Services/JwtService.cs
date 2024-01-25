using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;


namespace FrontAppBlazor.Services
{

  public class JwtService
  {
    private ProtectedLocalStorage _sessionStorage;
    public JwtService(ProtectedLocalStorage sessionStorage)
    {
      _sessionStorage = sessionStorage;
    }

    private const string Key = "YourSecretKeyLongLongLongLongEnough";

    public async Task<string> GetAndVerifyTokenAsync()
    {
      string token = await GetTokenFromLocalStorage();

      var tokenHandler = new JwtSecurityTokenHandler();
      var validationParameters = new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key)),
        ValidateIssuer = false,
        ValidateAudience = false
      };

      try
      {
        ClaimsPrincipal claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
        return token;
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Token validation failed: {ex.Message}");
        return null;
      }
    }

    private async Task<string> GetTokenFromLocalStorage()
    {
      var token = await _sessionStorage.GetAsync<string>("jwt");
      return token.Value;
    }

    public async Task<bool> IsAuthenticated()
    {
      string token = await GetAndVerifyTokenAsync();
      return token != null;
    }
  }
}
