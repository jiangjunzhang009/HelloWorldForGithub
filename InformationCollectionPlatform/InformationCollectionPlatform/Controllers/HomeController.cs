using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InformationCollectionPlatform.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            Services.ReceiveMessageService service = new Services.ReceiveMessageService();
            //service.TestDriveDevelop();
            return View();
        }
    }
}
