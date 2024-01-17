using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Payments_Itiel.Entities;
using Payments_Itiel.Models;

namespace Payments_Itiel.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        
        public UserController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email, };
            var result = await userManager.CreateAsync(user, model.Password);
            if(result.Succeeded)
            {
                await signInManager.SignInAsync(user, isPersistent: true);
            }
            return RedirectToAction("Index", "Home");
        }
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure:false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError(string.Empty, " Username or password incorrect");
                return View(model);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            // Puedes redirigir a la página de inicio u otra página después del logout
            return RedirectToAction("Login", "User");
        }
    }
}
