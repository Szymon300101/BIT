using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebMVC.Helpers
{
    public static class CookiesHelper
    {
        public static UserRoleEnum VerifyUserRole(HttpCookie cookie)
        {
            string value = cookie?["role"];

            if(value == null)
                return UserRoleEnum.none;

            if  (value.Equals("player"))
                return UserRoleEnum.player;
            if  (value.Equals("gm"))
                return UserRoleEnum.gm;

            return UserRoleEnum.none;
        }
    }
}