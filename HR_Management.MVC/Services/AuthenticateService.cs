using Hanssens.Net;
using HR_Management.MVC.Contracts;
using HR_Management.MVC.Models;
using HR_Management.MVC.Services.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace HR_Management.MVC.Services
{
    public class AuthenticateService : BaseHttpService, IAuthenticateService
    {
        private IHttpContextAccessor _httpContextAccessor;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly ILocalStorageService _localStorage;
        public AuthenticateService(IClient client, ILocalStorageService localStorageService, IHttpContextAccessor httpContextAccessor)
            : base(client, localStorageService)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            _localStorage = localStorageService;
        }

        public async Task<bool> Authenticate(LoginVM loginVM)
        {
            try
            {
                AuthRequest authRequest = new AuthRequest()
                {
                    Email = loginVM.Email,
                    Password = loginVM.Password
                };
                var authenticateResponse = await _client.LoginAsync(authRequest);
                if (authenticateResponse.Token != string.Empty)
                {
                    var tokenContent = _jwtSecurityTokenHandler.ReadJwtToken(authenticateResponse.Token);
                    var claims = ParseClaims(tokenContent);
                    var user = new ClaimsPrincipal(new ClaimsIdentity(claims,
                        CookieAuthenticationDefaults.AuthenticationScheme));
                    var login =  _httpContextAccessor.HttpContext.
                        SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,user);

                    _localStorage.SetStorageValue("token", authenticateResponse.Token);
                    return true;
                }
            }
            catch
            {
                
            }
            return false;
        }

        public async Task Logout()
        {
            _localStorage.ClearStorage(new List<string> { "token" });
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        public async Task<bool> Register(RegisterVM registerVM)
        {
            RegistrationRequest registrationRequest = new RegistrationRequest()
            {
                Email = registerVM.Email,
                Password = registerVM.Password,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                UserName = registerVM.UserName

            };
            var response = await _client.RegisterAsync(registrationRequest);
            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;    
            }
            return false;
        }

        private IList<Claim> ParseClaims(JwtSecurityToken token)
        {
            var claims = token.Claims.ToList();
            claims.Add(new Claim(ClaimTypes.Name, token.Subject));
            return claims;
        }
    }
}
