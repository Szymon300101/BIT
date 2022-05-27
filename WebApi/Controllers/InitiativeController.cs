using BackgroundLogic.InputOutput;
using BackgroundLogic.Models;
using Microsoft.AspNetCore.Mvc;
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
    }
}
