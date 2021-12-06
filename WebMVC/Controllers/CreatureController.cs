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
using WebMVC.Helpers;

namespace WebMVC.Controllers
{
    public class CreatureController : Controller
    {
        public ActionResult Delete(int id)
        {
            //usuwanie
            CreatureIO.DeleteRecord(id);
            StateData.InitSyncMenager.CallForSync();
            return RedirectToAction("Index", "Initiative");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreatureModel model)
        {
            if (ModelState.IsValid)
            {
                CreatureIO.AddRecord(model);
                StateData.InitSyncMenager.CallForSync();
            }


            return RedirectToAction("Index", "Initiative");
        }


        public ActionResult AddtoInit(int id)
        {
            CreatureModel model = CreatureIO.Select(id);

            InitiativeIO.AddRecord(model);
            StateData.InitSyncMenager.CallForSync();
            StateData.BMSyncMenager.CallForSync();


            return RedirectToAction("Index", "Initiative");
        }

        public ActionResult SaveImg()
        {
            Stream file = Request.Files[0].InputStream;
            
            string path = $"/ImageBase/Creatures/{file.GetHashCode()}.png";
            string fullPath = FileIO.GetProgDataPath(path);

            using (var fileStream = System.IO.File.Create(fullPath))
            {
                file.Seek(0, SeekOrigin.Begin);
                file.CopyTo(fileStream);
            }


            var contents = new { path = path};

            return Json(contents, JsonRequestBehavior.AllowGet);
        }
        
    }
}