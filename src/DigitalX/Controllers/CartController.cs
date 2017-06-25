using DigitalX.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using DigitalX.Models;

namespace DigitalX.Controllers
{
    public class CartController : Controller
    {
        ProductServiceClient psc = new ProductServiceClient();

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
                List<Item> cart = (List<Item>)Session["cart"];
                int index = isExisting(id);
                if (index == -1)
                    cart.Add(new Item(psc.find(id), 1));
                else
                    cart[index].Quantity++;
                Session["cart"] = cart;
            }

            return View("Cart");
        }        
        
        public ActionResult checkout()
        {   
            var username = User.Identity.Name;
            var cust = psc.findCustomer(username);
            custid = cust.CustomerID;
            ViewBag.addressList = psc.findAllAddress(username);            
            
            //if(addresslist == null)
            //{
            //  return CreateAddress();
            //}  
            //else
            //{  
                return View();
            //}
        }

        public ActionResult BOorCont()
        {
            backord = true;
            return View();
        }

        public ActionResult createBackOrder()
        {
            backord = false;
            return RedirectToAction("invoice", "Cart");
        }

        public int ordid { get; set; }
        public int custid { get; set; }
        public int addid { get; set; }
        public bool backord { get; set; }
        public int prodid { get; set; }
        public int prodqty { get; set; }
        public Address addy { get; set; }


        public ActionResult createOrder()
        {
            

            if (ModelState.IsValid)
            {
                addid = psc.findAddid();
                var username = User.Identity.Name;
                var cust = psc.findCustomer(username);
                custid = cust.CustomerID;
                backord = true;
                var order = new Order();
                order.CustomerID = custid;                
                order.OrderDate = DateTime.Now;
                order.Complete = backord;
                order.AddressID = addid;

                psc.createOrder(order);

                ordid = psc.findOrdid();
                
                List<Item> cart = (List<Item>)Session["cart"];                

                foreach(var item in cart)
                {
                    //Item res = new Item();
                    //res.Product = item.Product;
                    //res.Quantity = item.Quantity;

                    prodid = item.Product.ProductID;
                    prodqty = item.Quantity;

                    var orderDetails = new OrderDetail();
                    orderDetails.OrderID = ordid;
                    orderDetails.ProductID = prodid;
                    orderDetails.Quantity = prodqty;

                    psc.createOrderDetails(orderDetails);
                }

                //Product product = (Product)Session["cart"];
                //prodid = product.ProductID;
                //int index = isExisting(prodid);
                //prodqty = cart[index].Quantity;

                //return RedirectToAction("invoice", "Cart");
                //    }
                //    return RedirectToAction("checkout", "Cart");
                //}

                //[HttpPost]
                //public ActionResult createOrderDetails(OrderViewModel model)
                //{
                //    if (ModelState.IsValid)
                //    {

                //var orderDetails = new OrderDetail();
                //orderDetails.OrderID = ordid;
                //orderDetails.ProductID = prodid;
                //orderDetails.Quantity = prodqty;

                //psc.createOrderDetails(orderDetails);
                return RedirectToAction("invoice", "Cart");
            }
            return RedirectToAction("checkout", "Cart");
        }

        public ActionResult invoice()
        {
            var username = User.Identity.Name;
            addy = (Address)Session["address"];
            ViewBag.displayAddress = addy;
            ordid = psc.findOrdid();
            ViewBag.order = psc.invoiceOrder(ordid);
            


            ViewBag.customerDetails = psc.findCustomer(username);

            return View();
        }  

        public ActionResult CreateAddress()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateAddress(CreateAddressViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var username = User.Identity.Name;
                //Customer cust = psc.findCustomer(username);


                var address = new Address();
                address.Street = model.Street;
                address.Suburb = model.Suburb;
                address.City = model.City;
                address.Country = model.City;
                address.PostalCode = model.PostalCode;
                address.AddressType = 1;               

                psc.createAddress(address);                               
                

                Session["address"] = address;

                return RedirectToAction("createOrder", "Cart");
            }
            return View(model);
        }

        //[HttpPost]
        //public ActionResult checkout(CreateAddressViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        List<Item> cart = (List<Item>)Session["cart"];
        //        var test = cart.
        //        var order = new Order { CustomerID = cart. };
        //        return View(model);
        //    }
        //}
    }
}