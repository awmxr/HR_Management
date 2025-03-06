using HR_Management.MVC.Contracts;
using HR_Management.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace HR_Management.MVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAuthenticateService _authenticateService;

        public UsersController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }

        #region Register

        public async Task<IActionResult> Register()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Registration Faild.");
                return View(register);
            }

            var isRegistered = await _authenticateService.Register(register);
            if (isRegistered)
            {
                return LocalRedirect("/");
            }
            ModelState.AddModelError("", "Registration Faild.");
            return View(register);
        }


        #endregion

        #region Login

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login, string returnUrl)
        {
            returnUrl ??= Url.Content("~/");
            var isLoggedIn = await _authenticateService.Authenticate(login);
            if (isLoggedIn)
            {
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("", "Login Faild, Please Try Again.");
            return View(login);
        }
        #endregion


        #region Logout
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _authenticateService.Logout();
            return LocalRedirect("~/Users/Login");
        }
        #endregion
    }
}
