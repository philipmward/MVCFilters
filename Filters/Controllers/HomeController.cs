using Filters.Infrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Permissions;
using System.Web;
using System.Web.Mvc;

namespace Filters.Controllers
{
    public class HomeController : Controller
    {
        private Stopwatch timer;

        //[CustomAction]
        [ProfileAction]
        [ProfileResult]
        [ProfileAll]
        public string FilterTest()
        {
            return "This is the FilterTest action";
        }

        //[CustomAuth(false)]
        [Authorize(Users = "admin")]
        public string Index()
        {
            return "This is the Index action on the Home controller";
        }

        [GoogleAuth]
        [Authorize(Users = "bob@google.com")]
        public string List()
        {
            return "This is the List action on the Home controller";
        }

        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "RangeError")]
        //[RangeException]
        public string RangeTest(int id)
        {
            if (id > 100)
            {
                return String.Format("The id value is: {0}", id);
            }
            throw new ArgumentOutOfRangeException("id", id, "");
        }

        //protected override void OnActionExecuting(ActionExecutingContext filtContext)
        //{
        //    timer = Stopwatch.StartNew();
        //}

        //protected override void OnResultExecuted(ResultExecutedContext filterContext)
        //{
        //    timer.Stop();
        //    filterContext.HttpContext.Response.Write(string.Format("<div>Total elapsed time: {0:F6}</div>", timer.Elapsed.TotalSeconds));
        //}
    }
}