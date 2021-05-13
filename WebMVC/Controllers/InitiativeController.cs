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
            try
            {
                InitiativeIO.DeleteRecord(id);
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                TempData["ErrorMore"] = " STACK TRACE: " + e.StackTrace;
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatureModel model)
        {
            var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(model.Name)) throw new Exception("Nazwa stworzenia nie może być pusta");

                    InitiativeIO.AddRecord(model);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                    TempData["ErrorMore"] = " STACK TRACE: " + e.StackTrace;
                }
            }else
            {
                TempData["ErrorMessage"] = "Błąd formulaża.";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CreatureModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.PositionX = -1;
                    model.PositionY = -1;
                    InitiativeIO.UpdateRecord(model);
                }
                catch (Exception e)
                {
                    TempData["ErrorMessage"] = e.Message;
                    TempData["ErrorMore"] = " STACK TRACE: " + e.StackTrace;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Błąd formulaża.";
            }


            return RedirectToAction("Index");
        }



        public ActionResult Clear()
        {
            try
            {
                List<CreatureModel> records = InitiativeIO.GetInitiative();

                foreach (var item in records)
                {
                    InitiativeIO.DeleteRecord(item.Id);
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                TempData["ErrorMore"] = " STACK TRACE: " + e.StackTrace;
            }

            return RedirectToAction("Index");
        }
        public ActionResult RerollAll()
        {
            try
            {
                List<CreatureModel> records = InitiativeIO.GetInitiative();

                foreach (var item in records)
                {
                    item.Initiative = 0;
                    InitiativeIO.UpdateRecord(item);
                }
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                TempData["ErrorMore"] = " STACK TRACE: " + e.StackTrace;
            }

            return RedirectToAction("Index");
        }



    }
}