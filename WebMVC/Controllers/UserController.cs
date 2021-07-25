using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMVC.Helpers;

namespace WebMVC.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            TempData["role"] = UserRoleEnum.none;
            return View();
        }


        public ActionResult SelectPlayer()
        {
            // Create the cookie object.
            HttpCookie cookie = new HttpCookie("BIT");
            cookie["role"] = "player";
            // This cookie will remain  for one month.
            cookie.Expires = DateTime.Now.AddDays(1);

            // Add it to the current web response.
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "BattleMap") ;
        }

        public ActionResult SelectGM()
        {
            // Create the cookie object.
            HttpCookie cookie = new HttpCookie("BIT");
            cookie["role"] = "gm";
            // This cookie will remain  for one month.
            cookie.Expires = DateTime.Now.AddDays(1);

            // Add it to the current web response.
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Initiative");
        }


        public ActionResult Logout()
        {
            HttpCookie cookie = new HttpCookie("BIT");
            cookie["role"] = "none";
            // This cookie will remain  for one month.
            cookie.Expires = DateTime.Now.AddDays(1);

            // Add it to the current web response.
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index");
        }
    }
}