using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMVC.Controllers
{
    public class CreatureController : Controller
    {
        public ActionResult Delete(int id)
        {
            //usuwanie
            CreatureIO.DeleteRecord(id);
            return RedirectToAction("Index", "Initiative");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatureModel model)
        {
            if (ModelState.IsValid)
            {
                CreatureIO.AddRecord(model);
            }


            return RedirectToAction("Index", "Initiative");
        }


        public ActionResult AddtoInit(int id)
        {
            CreatureModel model = CreatureIO.Select(id);

            InitiativeIO.AddRecord(model);


            return RedirectToAction("Index", "Initiative");
        }
    }
}