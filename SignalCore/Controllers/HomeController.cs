using Microsoft.AspNetCore.Mvc;

namespace SignalCore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Redirect()
        {
            return RedirectToPage("/Index");
        }
    }
}
