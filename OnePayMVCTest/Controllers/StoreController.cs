using OnePayMVCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnePayMVCTest.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            List<Product> cart = new List<Product>();
            cart.Add(new Product(
                imagePath: "",
                name: "Test",
                price: 10,
                quantity: 1
                ));
            cart.Add(new Product(
                imagePath: "",
                name: "Test2",
                price: 20,
                quantity: 2
                ));
            ViewBag.Cart = cart;
            return View();
        }
    }
}