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
    public class BattleMapController : Controller
    {
        // GET: BattleMap
        public ActionResult Index()
        {
            List<CreatureModel> initiativeRecords = InitiativeIO.GetInitiative();
            BattleMapModel battlemapRecord = BattleMapIO.GetData();

            BattleMapTransViewModel model = new BattleMapTransViewModel();

            model.FullInitiative = initiativeRecords;
            model.StateData = battlemapRecord;


            return View(model);

        }
    }
}