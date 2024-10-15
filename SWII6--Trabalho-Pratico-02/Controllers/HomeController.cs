using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using tp2tuller.Models;

namespace tp2tuller.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
