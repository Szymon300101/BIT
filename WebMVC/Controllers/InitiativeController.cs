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
            List<CreatureModel> initiativeRecords = InitiativeIO.GetInitiative();
            List<CreatureModel> creatureRecords = CreatureIO.GetData();
            InitaitiveTransViewModel model = new InitaitiveTransViewModel();
            model.Data = initiativeRecords;
            model.CreatureList = creatureRecords;

            List<CreatureModel> SortedList = model.Data.OrderByDescending(o => o.Initiative).ToList();
            model.Data = SortedList;

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            //usuwanie
            InitiativeIO.DeleteRecord(id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatureModel model)
        {
            if (ModelState.IsValid)
            {
                InitiativeIO.AddRecord(model);
            }


            return RedirectToAction("Index");
        }

        
    }
}