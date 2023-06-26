using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Muson.Controllers
{
    public class AboutController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

    }
}
