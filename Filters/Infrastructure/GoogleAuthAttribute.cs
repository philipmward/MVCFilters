using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace Filters.Infrastructure
{
    public class GoogleAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        //Runs OnAuthentication, if there is a result added it will run the OnAuthenticationChallenge to try to authenticate
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            //does not have to be implemented
            IIdentity identity = filterContext.Principal.Identity;
            if (!identity.IsAuthenticated || !identity.Name.EndsWith("@google.com"))
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            if (filterContext.Result == null || filterContext.Result is HttpUnauthorizedResult)
            {
                //Doesn't have the abstractions that a controller has to return the RedirectToRoute Action Method, so we have to implement it the long hand way.
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    {"controller", "GoogleAccount"},
                    {"action", "Login"},
                    {"returnUrl", filterContext.HttpContext.Request.RawUrl}
                });
            }

            //only put the below in for temporarily elevated authentication that needs to be removed after the method is called. OnAuthenticationChallenge is called twice in
            //an action method, once at the start and once right before returning the action result. Which is the reason for the Result null check above, to filter out double
            //challenging. So the below would effectively sign them out at the completion of the method.
            else
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}