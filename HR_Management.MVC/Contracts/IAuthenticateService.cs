using HR_Management.MVC.Models;

namespace HR_Management.MVC.Contracts
{
    public interface IAuthenticateService
    {
        Task<bool> Authenticate(LoginVM loginVM );

        Task<bool> Register(RegisterVM registerVM);
        Task Logout();
    }
}
