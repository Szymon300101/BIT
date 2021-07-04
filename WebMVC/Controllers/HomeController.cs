using BackgroundLogic.InputOutput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Helpers;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var testData = InitiativeIO.GetInitiative();

            return RedirectToAction("Index", "Initiative");
        }
    }
}