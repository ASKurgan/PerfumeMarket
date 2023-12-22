using Microsoft.AspNetCore.Mvc;
using PerfumeMarket.Service.Interfaces;

namespace PerfumeMarket.Controllers
{
    public class BasketController : Controller
    {

        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public IActionResult Detail()
        {
            var response = _basketService.GetItems(User.Identity.Name);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return View(response.Data.ToList());
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult GetItem(long id)
        {
            var response = _basketService.GetItem(User.Identity.Name, id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return PartialView(response.Data);
            }
            return RedirectToAction("Index", "Home");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
