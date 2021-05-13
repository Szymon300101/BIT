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
            const int width = 33;
            int x = id  %  width;
            int y = id / width;


            BattleMapModel battlemapRecord = BattleMapIO.GetData();
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
    }
}