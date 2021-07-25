using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Helpers;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class InitiativeController : Controller
    {
        public ActionResult Index()
        {
            //weryfikacja roli
            UserRoleEnum role = CookiesHelper.VerifyUserRole(Request.Cookies["BIT"]);
            TempData["role"] = role;
            if(role == UserRoleEnum.player)
                return RedirectToAction("Index", "BattleMap");
            else if(role == UserRoleEnum.none)
                return RedirectToAction("Index", "User");
            //role = (UserRoleEnum)TempData.Peek("role");

            //odczyt danych z bazy
            List<CreatureModel> initiativeRecords = InitiativeIO.GetInitiative();
            List<CreatureModel> creatureRecords = CreatureIO.GetData();

            //układanie rekordów w tras model
            InitaitiveTransViewModel model = new InitaitiveTransViewModel();
            model.Data = initiativeRecords.OrderByDescending(o => o.Initiative).ToList();
            model.CreatureList = creatureRecords;

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            try
            {
                InitiativeIO.DeleteRecord(id);
                StateData.InitSyncMenager.CallForSync();
                StateData.BMSyncMenager.CallForSync();
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
            //var errors = ModelState.Select(x => x.Value.Errors)
            //               .Where(y => y.Count > 0)
            //               .ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(model.Name)) throw new Exception("Nazwa stworzenia nie może być pusta");

                    InitiativeIO.AddRecord(model);

                    StateData.InitSyncMenager.CallForSync();
                    StateData.BMSyncMenager.CallForSync();
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

        //zmiana rekordu inicjatywy z poziomu tabelki
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CreatureModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //pozycja ustawiona na (-1,-1) oznacza, że pozostanie ona nie zmieniona
                    model.PositionX = -1;
                    model.PositionY = -1;
                    InitiativeIO.UpdateRecord(model);
                    StateData.InitSyncMenager.CallForSync();
                    StateData.BMSyncMenager.CallForSync();
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
                //pobranie rekordów z bazy danych
                List<CreatureModel> records = InitiativeIO.GetInitiative();

                foreach (var item in records)
                {
                    InitiativeIO.DeleteRecord(item.Id);
                }
                StateData.InitSyncMenager.CallForSync();
                StateData.BMSyncMenager.CallForSync();
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
                    //inicjtywa ustawiona na 0 sprawi że UpdateRecord ją przelosuje
                    item.Initiative = 0;
                    InitiativeIO.UpdateRecord(item);
                }
                StateData.InitSyncMenager.CallForSync();
                StateData.BMSyncMenager.CallForSync();
            }
            catch (Exception e)
            {
                TempData["ErrorMessage"] = e.Message;
                TempData["ErrorMore"] = " STACK TRACE: " + e.StackTrace;
            }

            return RedirectToAction("Index");
        }

        public void SyncInit()
        {
            Response.ContentType = "text/event-stream";

            int id = StateData.InitSyncMenager.Subscribe();

            DateTime startDate = DateTime.Now;
            while (startDate.AddMinutes(10) > DateTime.Now)
            {
                Response.Write(string.Format("data: {0}\n\n", StateData.InitSyncMenager.IsNotSynced(id).ToString()));

                try
                {
                    Response.Flush();
                }
                catch (Exception)
                {
                    StateData.InitSyncMenager.Unsubscribe(id);
                    return;
                }

                System.Threading.Thread.Sleep(500);
            }

            Response.Close();
        }

    }
}