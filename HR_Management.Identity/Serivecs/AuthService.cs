using HR_Management.Application.Constants;
using HR_Management.Application.Contract.Identity;
using HR_Management.Application.Models.Identity;
using HR_Management.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HR_Management.Identity.Serivecs;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly JwtSettings _jwtSettings;
    public AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings
        )
    {
        _jwtSettings = jwtSettings.Value;
        _userManager = userManager;
        _signInManager = signInManager;
    }


    #region Register

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName);
        if (existingUser != null)
        {
            throw new Exception($"user name '{request.UserName}' already exist!");
        }

        var user = new ApplicationUser
        {
            UserName = request.UserName,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = true
        };


        var existingEmail = await _userManager.FindByEmailAsync(request.Email);
        if (existingEmail == null)
        {
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Employee");
                return new RegistrationResponse
                {
                    UserId = user.Id
                };

            }
            else
            {
                throw new Exception($"{result.Errors}");
            }

        }
        else
        {
            throw new Exception($"Email '{request.Email}' already exist!");
        }
    }
    #endregion

    public async Task<AuthResponse> Login(AuthRequest request)
    {
        throw new NotImplementedException();

    }


    private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
    {

        var userClaims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);

        var roleClaims = new List<Claim>();

        foreach (var role in roles)
        {
            roleClaims.Add(new Claim(ClaimTypes.Role, role));
        }

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email),
            new Claim(CustomClaimTypes.Uid,user.Id)

        }.Union(userClaims)
        .Union(roleClaims);

        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken
        (
             issuer: _jwtSettings.Issuer,
             audience: _jwtSettings.Audience,
             claims: claims,
             signingCredentials: signingCredentials,
             expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes)
        );

        return jwtSecurityToken;
    }
}
