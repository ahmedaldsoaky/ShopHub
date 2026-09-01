using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.DTOs.Account;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.Web.ViewModels.Account;

namespace myshop.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;


        public AccountController(
            IAccountService accountService,
            IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToHomeByRole(User.IsInRole(Roles.Admin)
                    ? Roles.Admin
                    : Roles.Customer);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var registerDto = _mapper.Map<RegisterDto>(model);

            var result = await _accountService.RegisterAsync(registerDto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error);

                return View(model);
            }

            return RedirectToHomeByRole(result.Role);
        }

        #endregion

        #region Login

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToHomeByRole(User.IsInRole(Roles.Admin)
                    ? Roles.Admin
                    : Roles.Customer);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var loginDto = _mapper.Map<LoginDto>(model);
            var result = await _accountService.LoginAsync(loginDto);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty,
                    "Your account has been locked due to multiple failed login attempts.");
                return View(model);
            }

            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error);
                return View(model);
            }

            return RedirectToHomeByRole(result.Role);
        }

        #endregion

        #region Logout

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();

            return RedirectToAction(nameof(Login));
        }

        #endregion

        #region Helpers

        private IActionResult RedirectToHomeByRole(string role)
        {
            if (role == Roles.Admin)
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
        
        #endregion
    }
}