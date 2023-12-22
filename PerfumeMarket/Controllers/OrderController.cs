using Microsoft.AspNetCore.Mvc;
using PerfumeMarket.Domain.Entity;
using PerfumeMarket.Domain.Response;
using PerfumeMarket.Domain.ViewModel.Order;
using PerfumeMarket.Service.Interfaces;

namespace PerfumeMarket.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult CreateOrder(long id)
        {
            var orderModel = new CreateOrderViewModel()
            {
                PerfumeId = id,
                Login = User.Identity.Name,
                Quantity = 0
            };
            return View(orderModel);
        }

        [HttpPost]
        public IActionResult CreateOrder(CreateOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBaseResponse<Order>? response = _orderService.Create(model);
                if (response.StatusCode == Domain.Enum.StatusCode.OK)
                {
                    return Json(new { description = response.Description });
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public IActionResult Delete(int id)
        {
            var response = _orderService.Delete(id);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return RedirectToAction("Index", "Home");
            }
            return View("Error", $"{response.Description}");
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
