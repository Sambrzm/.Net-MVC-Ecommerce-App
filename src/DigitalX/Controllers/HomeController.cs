using DigitalX.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalX.Controllers
{
    public class HomeController : Controller
    {
        private ProductServiceClient psc = new ProductServiceClient();

        public ActionResult Index()
        {
            ViewBag.topFive = psc.topFive();
            return View();
        }

        public ActionResult Products()
        {
            ViewBag.Message = "List of Products.";

            ViewBag.listProducts = psc.findAll();

            return View();
        }

        public ActionResult Details(int Id = 0)
        {
            ViewBag.Message = "Product Details.";

            Product product = psc.find(Id);

            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}