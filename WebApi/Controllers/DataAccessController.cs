using BackgroundLogic.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class DataAccessController : Controller
    {

        /// <summary>
        /// Zwraca 'src' obrazka (png), które można urzyć w html'u
        /// </summary>
        /// <param name="path">Ścieżka bezwzględna obrazka w formacie 'png'</param>
        /// <returns></returns>
        [HttpPost(Name = "GetImg")]
        public Object GetImg([FromForm] string path)
        {
            string error = "";
            string errorMessage = "";

            if (path == null)
            {
                path = PathLookup.GetProgDataPath("noImage.png");
            }

            if (!System.IO.File.Exists(path))
            {
                path = PathLookup.GetProgDataPath("noImage.png");
            }

            using (var srcImage = Image.FromFile(path))
            {
                using (var streak = new MemoryStream())
                {
                    srcImage.Save(streak, ImageFormat.Png);
                    //return File(streak.ToArray(), "image/png");
                    return new { url = $"data:image/png;base64,{Convert.ToBase64String(streak.ToArray())}", error = error, errorMessage = errorMessage };
                }
            }
        }
    }
}
