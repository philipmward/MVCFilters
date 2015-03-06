using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters.Infrastructure
{
    public class ProfileResultAttribute : FilterAttribute, IResultFilter
    {
        private Stopwatch timer;

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop();
            filterContext.HttpContext.Response.Write(string.Format("<div>Result elapsedtime: {0:F6}</div>", timer.Elapsed.TotalSeconds));
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }
    }
}