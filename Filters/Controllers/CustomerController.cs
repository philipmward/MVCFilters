using Filters.Infrastructure;
using System.Web.Mvc;

namespace Filters.Controllers
{
    [SimpleMessage(Message = "A")]
    public class CustomerController : Controller
    {
        //The Order property specifies the order of execution. You rarely need this as it rarely matters the order.
        //[SimpleMessage(Message = "A", Order=1)]
        //[SimpleMessage(Message = "B", Order=2)]
        public string Index()
        {
            return "This is the Cutomer controller";
        }

        //An override filter will allow the filter listed to override all before instead of applying all previous and present filters of this type.
        [CustomOverrideActionFilters]
        [SimpleMessage(Message = "B")]
        public string OtherAction()
        {
            return "This is the Other Action in the Customer controller";
        }
    }
}