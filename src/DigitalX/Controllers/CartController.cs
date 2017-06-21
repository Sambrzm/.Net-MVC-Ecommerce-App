using DigitalX.ServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DigitalX.Controllers
{
    public class CartController : Controller
    {
        private WCFServiceClient psc = new WCFServiceClient();

        public ActionResult Index()
        {            
            if (Session["cart"] == null)
            {
                return View("EmptyCart");
            }
            else
            {
                return View("Cart");
            }                
        }

        private int isExisting(int id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.ProductID == id)
                    return i;
            return -1;
        }

        public ActionResult Remove(int id)
        {
            int index = isExisting(id);
            List<Item> cart = (List<Item>)Session["cart"];
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return View("cart");
        }

        [Authorize]
        public ActionResult AddToCart(int id)
        {
            if (Session["cart"] == null)
            {
                List<Item> cart = new List<Item>();
                cart.Add(new Item(psc.find(id), 1));
                Session["cart"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>) Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Item(psc.find(id), 1));
                else
                    cart[index].Quantity++;
                Session["cart"] = cart;
            }

            return View("Cart");
        }
    }
}