using BackgroundLogic.Helpers;
using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

        public ActionResult SetToMove(int id)
        {
            BattleMapModel battlemapRecord = BattleMapIO.GetData();
            battlemapRecord.MovingId = id;

            BattleMapIO.UpdateRecord(battlemapRecord);

            return RedirectToAction("Index");
        }

        public ActionResult Move(int id)
        {
            BattleMapModel battlemapRecord = BattleMapIO.GetData();

            int x = id  %  battlemapRecord.Width;
            int y = id / battlemapRecord.Width;


            CreatureModel creature = InitiativeIO.GetInitiative().Find(item => item.Id == battlemapRecord.MovingId);
            if(creature!=null)
            {
                creature.PositionX = x;
                creature.PositionY = y;
                InitiativeIO.UpdateRecord(creature);
            }
            battlemapRecord.MovingId = 0;
            BattleMapIO.UpdateRecord(battlemapRecord);


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
    }
}