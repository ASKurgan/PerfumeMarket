using GoogleApi.Entities;
using Microsoft.AspNetCore.Mvc;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Profile;
using PerfumeMarket.Service.Interfaces;

namespace PerfumeMarket.Controllers
{
    public class ProfileController : Controller
    {

        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [HttpPost]
        public IActionResult Save(ProfileViewModel model)
        {
            ModelState.Remove("Id");
            ModelState.Remove("UserName");
            if (ModelState.IsValid)
            {
                var response = _profileService.Save(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public IActionResult Detail()
        {
            string? userName = User.Identity.Name;
            BaseResponse<ProfileViewModel>? response = _profileService.GetProfile(userName);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View();
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
