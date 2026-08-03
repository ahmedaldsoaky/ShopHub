using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.Common;
using myshop.Entities.Models;
using myshop.Web.ViewModels.Account;

namespace myshop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToHomeByRole();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new ApplicationUser
            {
                FullName = model.FullName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }

            await _userManager.AddToRoleAsync(user, Roles.Customer);

            await _signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToHomeByRole();
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToHomeByRole();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(
                model.UserName,
                model.Password,
                model.RememberMe,
                lockoutOnFailure: true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty,
                    "Your account has been locked due to multiple failed login attempts.");

                return View(model);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty,
                    "Invalid username or password.");

                return View(model);
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            return await RedirectToHomeByRoleAsyncِ(user!);
        }

        #endregion

        #region Logout

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Helpers

        private IActionResult RedirectToHomeByRole()
        {
            if (User.IsInRole(Roles.Admin))
            {
                return RedirectToAction(
                    actionName: "Index",
                    controllerName: "Product",
                    routeValues: new { area = "Admin" });
            }

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Home");
        }

        private async Task<IActionResult> RedirectToHomeByRoleAsyncِ(ApplicationUser user)
        {
            if (await _userManager.IsInRoleAsync(user, Roles.Admin))
            {
                return RedirectToAction(
                    "Index",
                    "Product",
                    new { area = "Admin" });
            }

            return RedirectToAction(
                "Index",
                "Home");
        }

        #endregion
    }
}