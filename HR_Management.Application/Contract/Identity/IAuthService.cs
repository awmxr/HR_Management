﻿using HR_Management.Application.Models.Identity;

namespace HR_Management.Application.Contract.Identity;

public interface IAuthService
{
    Task<AuthResponse> Login(AuthRequest request);
    Task<RegistrationResponse> Register(RegistrationRequest request);
}
