using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using myshop.Common;
using myshop.Entities.Models;

namespace myshop.Web.Controllers.Admin
{
    [Authorize(Roles = Roles.Admin)]
    public class UserController : Controller

    {
        private readonly IUserService _userService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userService = userService;
            _userManager = userManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData()
        {
            var users = await _userService.GetAllAsync();

            return Json(new
            {
                data = users
            });
        }

        [HttpPost]
        public async Task<IActionResult> Promote(string id)
        {
            var result = await _userService.PromoteAsync(id);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to promote user."
                });
            }

            return Json(new
            {
                success = true,
                message = "User promoted successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Demote(string id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var result = await _userService.DemoteAsync(id, currentUserId!);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to demote user."
                });
            }

            return Json(new
            {
                success = true,
                message = "User demoted successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Lock(string id)
        {
            var currentUserId = _userManager.GetUserId(User);
            var result = await _userService.LockAsync(id, currentUserId!);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to lock user."
                });
            }

            return Json(new
            {
                success = true,
                message = "User locked successfully."
            });
        }

        [HttpPost]
        public async Task<IActionResult> Unlock(string id)
        {
            var result = await _userService.UnlockAsync(id);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to unlock user."
                });
            }

            return Json(new
            {
                success = true,
                message = "User unlocked successfully."
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            var currentUserId = _userManager.GetUserId(User);

            var result = await _userService.DeleteAsync(id, currentUserId!);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                    message = "Unable to delete user."
                });
            }

            return Json(new
            {
                success = true,
                message = "User deleted successfully."
            });
        }
    }
}
