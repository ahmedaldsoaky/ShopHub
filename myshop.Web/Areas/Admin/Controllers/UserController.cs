using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using myshop.BLL.Interfaces;
using myshop.BLL.Services;
using myshop.Common;
using myshop.Entities.Models;

namespace myshop.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    [Area("Admin")]
    public class UserController : Controller

    {
        private readonly IUserService _userService;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GetData(PagedRequestDto requestDto)
        {
            var PagedUsers = await _userService.GetPagedAsync(requestDto);

            return Json(new
            {
                draw = Request.Query["draw"],
                data = PagedUsers.Data,
                recordsTotal = PagedUsers.TotalCount,
                recordsFiltered = PagedUsers.FilteredCount
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
            var result = await _userService.DemoteAsync(id, User);

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
            var result = await _userService.LockAsync(id, User);

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
            var result = await _userService.DeleteAsync(id, User);

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
