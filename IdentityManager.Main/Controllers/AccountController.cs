using IdentityManager.Library.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManager.Main.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var registerViewModel = new RegisterViewModel();
            return View(registerViewModel);

        }
        //[HttpPost]
        //public async Task<IActionResult> Register()
        //{
        //    //var registerViewModel = new RegisterViewModel();
        //    //return View(registerViewModel);
        //    return Ok();
        //}
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var loginViewModel = new LoginViewModel();
            return View(loginViewModel);

        }
    }
}
