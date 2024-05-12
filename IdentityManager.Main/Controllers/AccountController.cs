using IdentityManager.Library.Models.Entites;
using IdentityManager.Library.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Encodings.Web;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IdentityManager.Main.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public AccountController(UserManager<ApplicationUser> userManager,
                                 RoleManager<IdentityRole> roleManager,
                                 SignInManager<ApplicationUser> signInManager,
                                 IEmailSender emailSender)
        {
            _userManager=userManager;
            _roleManager=roleManager;
            _signInManager=signInManager;
            _emailSender=emailSender;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Register(string returnurl = null)
        {
            var registerViewModel = new RegisterViewModel();
            registerViewModel.ReturnUrl = returnurl;
            return View(registerViewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    Name = model.Name,
                    //DateCreated = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //return RedirectToAction("Index", "Home");
                    return LocalRedirect(model.ReturnUrl);
                }
                AddErrors(result);
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Login(string returnurl = null)
        {
            ViewData["ReturnUrl"] = returnurl;
            //returnurl = returnurl ?? Url.Content("~/");
            //ViewBag.ReturnUrl = returnUrl;
            var loginViewModel = new LoginViewModel();
            loginViewModel.ReturnUrl = returnurl;
            return View(loginViewModel);

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            //ViewData["ReturnUrl"] = returnurl;
            model.ReturnUrl = model.ReturnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result =await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //return RedirectToAction("Index", "Home");
                    return LocalRedirect(model.ReturnUrl);
                }
                else if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
        [HttpGet]
        public async Task<IActionResult> ForgotPassword(string returnurl = null)
        {
            ForgotPasswordViewModel forgotPasswordViewModel=new ForgotPasswordViewModel();
            forgotPasswordViewModel.ReturnUrl= returnurl;
            return View(forgotPasswordViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null)
                {
                    return RedirectToAction(nameof(ForgotPasswordError), new { message = "User could not be found.", returnUrl = model.ReturnUrl });
                }
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackurl = Url.Action("ResetPassword", "Account", new
                {
                    token,
                    userid = user.Id,                 
                    returnUrl = model.ReturnUrl
                }, protocol: HttpContext.Request.Scheme);
                callbackurl = HtmlEncoder.Default.Encode(callbackurl);
                await _emailSender.SendEmailAsync(model.Email, "Reset Password - Identity Manager",
                                       $"Please reset your password by clicking here: <a href='{callbackurl}'>link</a>");

                return RedirectToAction(nameof(ForgotPasswordEmailSendSuccess), new { returnUrl = model.ReturnUrl});
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> ForgotPasswordEmailSendSuccess([FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ForgotPasswordError(string message,string returnurl = null)
        {
            ViewBag.message = message;
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> ResetPassword(string token, string userId, string returnUrl)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return RedirectToAction(nameof(ResetPasswordError), new { message = "User could not be found.", returnUrl = returnUrl });
            }

            if (token is null)
            {
                return RedirectToAction(nameof(ResetPasswordError), new { message = "Token can not be empty.", returnUrl = returnUrl });
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));

            var tokenValidResult = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", token);
            if (tokenValidResult is false)
            {
                return RedirectToAction(nameof(ResetPasswordError), "Account", new { message = "Token is invalid.", returnUrl = returnUrl });
            }
            var model = new ResetPasswordViewModel { Token = token, Email = user.Email, UserId = userId, ReturnUrl = returnUrl };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.UserId);
                if (user is null)
                {
                    return RedirectToAction(nameof(ResetPasswordError), new { message = "User could not be found.", returnUrl = model.ReturnUrl });
                }

                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(model.Token));
                IdentityResult resetResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                if (resetResult.Succeeded)
                {
                    return RedirectToAction(nameof(ResetPasswordSuccess), new { returnUrl = model.ReturnUrl });
                    //return LocalRedirect(model.ReturnUrl);
                }

                AddErrors(resetResult);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPasswordSuccess([FromQuery] string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpGet]       
        public IActionResult ResetPasswordError([FromQuery] string message, [FromQuery] string returnUrl)
        {
            ViewBag.Error = message;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
