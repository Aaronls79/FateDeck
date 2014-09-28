using System.Web.Mvc;

namespace FateDeck.Web.Controllers
{
    public class HomeController : ControllerBase
    {
        public ActionResult About()
        {
            return View();
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}
