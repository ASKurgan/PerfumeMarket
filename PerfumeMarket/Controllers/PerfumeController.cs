using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Perfume;
using PerfumeMarket.Service.Interfaces;

namespace PerfumeMarket.Controllers
{
    public class PerfumeController(IPerfumeService perfumeService) : Controller
    {
        private readonly IPerfumeService _perfumeService = perfumeService;

        [HttpGet]
        public IActionResult GetPerfumes() 
        {
            IBaseResponse<List<Perfume>>? response = _perfumeService.GetPerfumes();
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data);
            }
            return View("Error", $"{response.Description}");
        }

        [Authorize(Roles ="Admin")]
        public IActionResult  Delete (int id)
        
        {
            IBaseResponse<bool>? response = _perfumeService.DeletePerfume(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("GetPerfumes");
            }
            return View("Error", $"{response.Description}");
        }

        public IActionResult Compare() => PartialView();

        [HttpGet]
        public IActionResult Save(int id) 
        {
            if (id == 0)
            {
                return PartialView();
            }
            IBaseResponse<PerfumeViewModel>? response = _perfumeService.GetPerfume(id);
            
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }

            ModelState.AddModelError("", response.Description);
            return PartialView();
        }

        [HttpPost]
        public IActionResult Save(PerfumeViewModel viewModel ) 
        {
            ModelState.Remove("Id");
            ModelState.Remove("DateCreate");

            if (ModelState.IsValid)
            {
                if (viewModel.Id == 0)
                {
                    byte[] imageData;
                    using (var binaryReader = new BinaryReader(viewModel.Avatar.OpenReadStream())) 
                    {
                        imageData = binaryReader.ReadBytes((int)viewModel.Avatar.Length);
                    }
                    _perfumeService.Create(viewModel, imageData);
                }

                else
                {
                    _perfumeService.Edit(viewModel.Id, viewModel);
                }

            }

            return RedirectToAction("GetPerfumes");
        }

        [HttpGet]
        public IActionResult GetPerfume (int id, bool isJson) 
        {
           IBaseResponse<PerfumeViewModel>? response = _perfumeService.GetPerfume(id);
            if (isJson)
            {
                return Json(response.Data);
            }

            return RedirectToAction ("GetPerfume", response.Data);
        }

        [HttpPost]
        public IActionResult GetPerfume(string term)
        {
            BaseResponse<Dictionary<long, string>>? response = _perfumeService.GetPerfume(term);
            return Json(response.Data);
        }

        [HttpPost]

        public JsonResult GetTypes()
        {
            BaseResponse<Dictionary<int, string>>? type = _perfumeService.GetTypes();
            return Json(type.Data);
        }
            
            
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
