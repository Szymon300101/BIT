using BackgroundLogic.Helpers;
using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using WebMVC.Helpers;
using WebMVC.Models;


namespace WebMVC.Controllers
{
    public class BattleMapController : Controller
    {
        // GET: BattleMap
        public ActionResult Index()
        {
            UserRoleEnum role = CookiesHelper.VerifyUserRole(Request.Cookies["BIT"]);
            TempData["role"] = role;
            if (role == UserRoleEnum.none)
                return RedirectToAction("Index", "User");
            //role = (UserRoleEnum)TempData.Peek("role");

            List<CreatureModel> initiativeRecords = InitiativeIO.GetInitiative();
            BattleMapModel battlemapRecord = BattleMapIO.GetData();

            BattleMapTransViewModel model = new BattleMapTransViewModel();

            model.FullInitiative = initiativeRecords.OrderByDescending(o => o.Initiative).ToList();
            model.StateData = battlemapRecord;

            return View(model);

        }
        public ActionResult GetData()
        {
            string errors = "";
            BattleMapTransViewModel model = new BattleMapTransViewModel();

            UserRoleEnum role = CookiesHelper.VerifyUserRole(Request.Cookies["BIT"]);
            TempData["role"] = role;
            if (role == UserRoleEnum.none)
            {
                errors = "Błąd: Użytkownik nie ma przypisanej roli.";
            }else
            {

                List<CreatureModel> initiativeRecords = InitiativeIO.GetInitiative();
                BattleMapModel battlemapRecord = BattleMapIO.GetData();

                model.FullInitiative = initiativeRecords.OrderByDescending(o => o.Initiative).ToList();
                model.StateData = battlemapRecord;
            }


            var contents = new { dataModel = model , errors = errors};

            return Json(contents, JsonRequestBehavior.AllowGet);

        }

        public ActionResult SetToMove(int id)
        {
            BattleMapModel battlemapRecord = BattleMapIO.GetData();
            battlemapRecord.MovingId = id;

            BattleMapIO.UpdateRecord(battlemapRecord);
            StateData.BMSyncMenager.CallForSync();


            return RedirectToAction("Index");
        }

        public ActionResult Move(int id)
        {
            BattleMapModel battlemapRecord = BattleMapIO.GetData();

            int x = id % battlemapRecord.Width;
            int y = id / battlemapRecord.Width;


            CreatureModel creature = InitiativeIO.GetInitiative().Find(item => item.Id == battlemapRecord.MovingId);
            if (creature != null)
            {
                creature.PositionX = x;
                creature.PositionY = y;
                InitiativeIO.UpdateRecord(creature);

            }
            battlemapRecord.MovingId = 0;
            BattleMapIO.UpdateRecord(battlemapRecord);
            StateData.BMSyncMenager.CallForSync();
            StateData.InitSyncMenager.CallForSync();

            return RedirectToAction("Index");
        }
        public ActionResult NewTurn()
        {
            BattleMapIO.NewTurn();
            StateData.BMSyncMenager.CallForSync();

            return RedirectToAction("Index");
        }

        public ActionResult Clear()
        {
            BattleMapIO.Clear();

            List<CreatureModel> initiative = InitiativeIO.GetInitiative();
            for (int i = 0; i < initiative.Count; i++)
            {
                initiative[i].PositionX = i;
                initiative[i].PositionY = 0;
                InitiativeIO.UpdateRecord(initiative[i]);
                StateData.InitSyncMenager.CallForSync();
                StateData.BMSyncMenager.CallForSync();
            }

            return RedirectToAction("Index");
        }

        public ActionResult AddBackground(AddBackgroundModel model)
        {

            BattleMapModel battlemapRecord = BattleMapIO.GetData();
            battlemapRecord.BackgroundPath = model.FilePath;
            battlemapRecord.Width = model.Width;
            battlemapRecord.Height = model.Height;
            BattleMapIO.UpdateRecord(battlemapRecord);
            StateData.BMSyncMenager.CallForSync();



            return RedirectToAction("Index");
        }

        /// <summary>
        /// Zwraca 'src' obrazka (png), które można urzyć w html'u
        /// </summary>
        /// <param name="path">Ścieżka bezwzględna obrazka w formacie 'png'</param>
        /// <returns></returns>
        public ActionResult GetImg(string path, CreatureTypeEnum type)
        {
            if (path == null || !System.IO.File.Exists(path))
            {
                if (type == CreatureTypeEnum.player)
                {
                    path = FileIO.GetProgDataPath("ImageBase/Default/Hero.png");
                }
                if (type == CreatureTypeEnum.enemy)
                {
                    path = FileIO.GetProgDataPath("ImageBase/Default/Monster.png");
                }
                if (type == CreatureTypeEnum.npc)
                {
                    path = FileIO.GetProgDataPath("ImageBase/Default/NPC.png");
                }
            }

            using (var srcImage = Image.FromFile(path))
            {
                using (var streak = new MemoryStream())
                {
                    srcImage.Save(streak, ImageFormat.Png);
                    return File(streak.ToArray(), "image/png");
                }
            }
        }

        public ActionResult GetImgSrc(string path, CreatureTypeEnum type = CreatureTypeEnum.player)
        {
            var contents = new { src = Url.Action("GetImg", new { path = FileIO.GetProgDataPath(path), type = type }) };

            return Json(contents, JsonRequestBehavior.AllowGet);
        }

        [OutputCache(Duration = 20, Location = OutputCacheLocation.Client)]
        public ActionResult GetBackground(string path)
        {
            if (path == null || !System.IO.File.Exists(path))
            {
                path = FileIO.GetProgDataPath("ImageBase/Background/Default/Default.png");
            }

            using (var srcImage = Image.FromFile(path))
            {
                using (var streak = new MemoryStream())
                {
                    srcImage.Save(streak, ImageFormat.Png);
                    return File(streak.ToArray(), "image/png");
                }
            }
        }
        public ActionResult SaveImg()
        {
            Stream file = Request.Files[0].InputStream;

            string path = $"/ImageBase/Background/Custom.png";
            string fullPath = FileIO.GetProgDataPath(path);
            using (var fileStream = System.IO.File.Create(fullPath))
            {
                file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(fileStream);
            }


            var contents = new { path = path };

            return Json(contents, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DmgCreature(DmgCreatureModel dmgCreatureModel)
        {
            List<CreatureModel> initiative = InitiativeIO.GetInitiative();
            int pos = initiative.FindIndex(item => item.Id == dmgCreatureModel.DmgCreatureId);
            CreatureModel thisCreature = initiative[pos];
            thisCreature.HP = thisCreature.HP - dmgCreatureModel.Dmg;


            if(thisCreature.HP <=0 && thisCreature.CreatureType == CreatureTypeEnum.enemy)
            {
                InitiativeIO.DeleteRecord(thisCreature.Id);
            }
            else
            {
                if (thisCreature.HP < 0)
                {
                    thisCreature.HP = 0;
                }
                InitiativeIO.UpdateRecord(thisCreature);
            }

            BattleMapModel battlemapRecord = BattleMapIO.GetData();
            battlemapRecord.MovingId = 0;

            BattleMapIO.UpdateRecord(battlemapRecord);

            StateData.BMSyncMenager.CallForSync();
            StateData.InitSyncMenager.CallForSync();



            return RedirectToAction("Index");
        }


        
        public void SyncBM()
        {
            Response.ContentType = "text/event-stream";

            int id = StateData.BMSyncMenager.Subscribe();

            DateTime startDate = DateTime.Now;
            while (startDate.AddMinutes(1) > DateTime.Now)
            {
                Response.Write(string.Format("data: {0}\n\n", StateData.BMSyncMenager.IsNotSynced(id).ToString()));

                try
                {
                    Response.Flush();
                }
                catch (Exception)
                {
                    StateData.BMSyncMenager.Unsubscribe(id);
                    return;
                }

                System.Threading.Thread.Sleep(500);
            }

            StateData.BMSyncMenager.Unsubscribe(id);
            Response.Close();
        }
    }
}