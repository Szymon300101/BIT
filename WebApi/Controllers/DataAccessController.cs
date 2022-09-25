using BackgroundLogic.Helpers;
using BackgroundLogic.Helpers.Interfaces;
using BackgroundLogic.InputOutput;
using BackgroundLogic.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using WebApi.Helpers;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class DataAccessController : Controller
    {
        readonly IPathMenager _pathLookup;
        readonly IImageLogic _imageLogic;
        public DataAccessController(IPathMenager pathLookup, IImageLogic imageLogic)
        {
            _pathLookup = pathLookup;
            _imageLogic = imageLogic;
        }

        /// <summary>
        /// Zwraca 'src' obrazka (png), które można użyć w html'u
        /// </summary>
        /// <param name="path">Ścieżka bezwzględna obrazka w formacie 'png'</param>
        [HttpPost(Name = "GetImg")]
        public Object GetImg([FromForm] string path)
        {
            AjaxErrorInfo error = new AjaxErrorInfo(); 
            string base64Image = "";

            try
            {
                path = _imageLogic.HandleBadImagePaths(path);
                base64Image = _imageLogic.GetBase64StringForImage(path);
            }
            catch (Exception e)
            {
                error = new AjaxErrorInfo(e);
            }

            return new { url = $"data:image/png;base64,{base64Image}", error = error};
        }
    }
}
