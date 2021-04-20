using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class InitiativeController : Controller
    {
        public ActionResult Index()
        {
            List<CreatureModel> initiativeRecords = JsonIO.GetInitiative();

            InitaitiveTransViewModel model = new InitaitiveTransViewModel();
            model.Data = initiativeRecords;


            return View(model);
        }
    }
}