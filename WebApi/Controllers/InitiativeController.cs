using BackgroundLogic.Helpers;
using BackgroundLogic.InputOutput;
using BackgroundLogic.InputOutput.Interfaces;
using BackgroundLogic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using WebApi.Helpers.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class InitiativeController : Controller
    {
        readonly IInitiativeIO _initiativeIO;
        readonly IInitiativeConverter _initiativeConverter;

        public InitiativeController(IInitiativeIO initiativeIO, IInitiativeConverter initiativeConverter)
        {
            _initiativeIO = initiativeIO;
            _initiativeConverter = initiativeConverter;
        }

        [HttpGet(Name = "GetInitiative")]
        public Object GetInitiative()
        {
            List<CreatureModel> rawData = _initiativeIO.SelectAll();
            List<InitiativeCRUDModel> viewModels = _initiativeConverter.ConvertListFromCreatures(rawData);

            return new { items = viewModels };
        }
    }
}
