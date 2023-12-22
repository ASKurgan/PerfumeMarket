using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerfumeMarket.Domain.Entity;
using System.Diagnostics;

namespace PerfumeMarket.Controllers
{
    public class HomeController : Controller
    {
       //  [Authorize(Roles = "Admin")]
        public IActionResult Index() => View();
    }
}
