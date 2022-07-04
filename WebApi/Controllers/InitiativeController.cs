using BackgroundLogic.Helpers;
using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class InitiativeController : Controller
    {

        [HttpGet(Name = "GetCreatures")]
        public Object GetCreatures()
        {
            List<CreatureModel> rawData = CreatureIO.GetData();
            List<CreatureCRUDModel> models = new List<CreatureCRUDModel>();
            foreach (CreatureModel creature in rawData)
                models.Add(new CreatureCRUDModel(creature));

            return new { items = models };
        }

        [HttpPost(Name = "SaveImg")]
        public async Task<object> SaveImgAsync([FromForm] IFormFile file)
        {
            string fullPath = $"{PathLookup.ProgData}{PathLookup.CreatureImages}/{file.GetHashCode()}.png";
            string error = "";
            string errorMessage = "";

            try
            {
                FileIO.CleanupImages();

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);

                    FileIO.FormatAndSaveImg(fullPath, memoryStream, 200);
                }
            }
            catch (Exception e)
            {
                error = e.Message;
                errorMessage = e.StackTrace ?? "";
            }
            

            return new { path = fullPath, error = error, errorMessage = errorMessage};
        }
    }
}
