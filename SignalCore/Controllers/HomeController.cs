using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using Microsoft.AspNetCore.Mvc;
using SignalCore.Helpers;

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


        public ActionResult GetInitaitive()
        {
            List<CreatureModel> initiative = new List<CreatureModel>();
            ErrorInfo? error = null;

            try
            {
                initiative = InitiativeIO.SelectAll().OrderByDescending(item => item.Initiative).ToList();
            }
            catch (Exception e)
            {
                error = new ErrorInfo(e.Message, "", e.StackTrace);
            }

            var contents = new { initiative = initiative, error = error };

            return Json(contents);
        }

    }
}
