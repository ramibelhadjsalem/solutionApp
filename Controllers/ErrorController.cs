using Microsoft.AspNetCore.Mvc;

namespace solutionApp.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Forbiden()
        {
            return View();
        }
    }
}
