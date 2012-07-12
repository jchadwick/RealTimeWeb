using System.Web.Mvc;

namespace RealTimeWeb.Controllers
{
    public class DemosController : Controller
    {
        public ActionResult Index(string name = "Index")
        {
            return View(name);
        }
    }
}
