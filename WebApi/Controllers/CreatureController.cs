using BackgroundLogic.Helpers;
using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Helpers.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class CreatureController : Controller
    {
        readonly ICreatureConverter _creatureConverter;
        readonly IPathMenager _pathLookup;

        public CreatureController(ICreatureConverter creatureConverter, IPathMenager pathLookup)
        {
            _creatureConverter = creatureConverter;
            _pathLookup = pathLookup;
        }


        [HttpGet(Name = "GetCreatures")]
        public Object GetCreatures()
        {
            List<CreatureModel> rawData = CreatureIO.GetData();
            List<CreatureCRUDModel> models = _creatureConverter.ConvertListFromLogicToCRUD(rawData);

            return new { items = models };
        }

        [HttpPost(Name = "SaveImg")]
        public async Task<object> SaveImgAsync([FromForm] IFormFile file)
        {
            string fullPath = $"{_pathLookup.ProgData}{_pathLookup.CreatureImages}/{file.GetHashCode()}.png";
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


            return new { path = fullPath, error = error, errorMessage = errorMessage };
        }
    }
}
