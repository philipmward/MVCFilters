using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Filters
{
    public class CustomAuthAttribute : AuthorizeAttribute
    {
        private readonly bool _localAllowed;

        public CustomAuthAttribute(bool allowedParam)
        {
            _localAllowed = allowedParam;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return !httpContext.Request.IsLocal || _localAllowed;
        }
    }
}