using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DaoTaoLaiXe.Models
{
    public class AuthorizeFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Session["UserLogged"] == null)
            {
                filterContext.HttpContext.Response.Redirect("/login");
            }
        }

    }
}