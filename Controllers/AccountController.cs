using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using solution.Data.Entities;
using solutionApp.Models;

namespace solutionApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public async Task<IActionResult> Login(LoginModel model)
        {
          
            if (ModelState.IsValid)
            {
                var user =  _userManager.Users.FirstOrDefault(x => x.Email == model.Email || x.UserName == model.Email);
               /* var user = await _userManager.FindByEmailAsync(model.Email);*/
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email is Invalid.");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");
                    return LocalRedirect("~/");

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid password");
                    return View(model);
                }
            }

            // If login fails, redisplay the login view with validation errors
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model, string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new AppUser()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {

                    var role = model.Role == "Technicien" ? "Technicien" : "User";
                    var roleResult = await _userManager.AddToRoleAsync(user, role);
                    if (roleResult.Succeeded) return LocalRedirect("~/account/login");


                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);


                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }


            }
            return View();
        }


        [HttpGet]
        public async  Task<IActionResult> Logout()
        {

            await HttpContext.SignOutAsync("Identity.Application");
            return LocalRedirect("~/account/login");
           
        }
    }
}
