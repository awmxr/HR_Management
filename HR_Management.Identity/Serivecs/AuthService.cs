using HR_Management.Application.Contract.Identity;
using HR_Management.Application.Models.Identity;
using HR_Management.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Management.Identity.Serivecs;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IOptions<JwtSettings> _jwtSettings;
    public AuthService(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        IOptions<JwtSettings> jwtSettings
        )
    {
        _jwtSettings = jwtSettings;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public Task<AuthResponse> Login(AuthRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<RegistrationResponse> Register(RegistrationRequest request)
    {
        var existingUser = await _userManager.FindByNameAsync(request.UserName );
        if(existingUser != null)
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
        if(existingEmail == null)
        {
            var result = await _userManager.CreateAsync(user,request.Password);
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
}
