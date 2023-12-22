using Microsoft.AspNetCore.Mvc;
using PerfumeMarket.Domain.Extentions;
using PerfumeMarket.Domain.ViewModel.User;
using PerfumeMarket.Service.Interfaces;
using System.Linq;

namespace PerfumeMarket.Controllers
{
    public class UserController : Controller
    {

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult GetUsers()
        {
            var response = _userService.GetUsers();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult DeleteUser(long id)
        {
            var response = _userService.DeleteUser(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetUsers");
            }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Save() => PartialView();

        [HttpPost]
        public IActionResult Save(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
                return BadRequest(new { errorMessage = response.Description });
            }
            string? errorMessage = ModelState.Values
                .SelectMany(v => v.Errors.Select(x => x.ErrorMessage)).ToList().Join();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }

        [HttpPost]
        public JsonResult GetRoles()
        {
            var types = _userService.GetRoles();
            return Json(types.Data);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
