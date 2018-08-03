using OnepayMVCTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnepayMVCTest.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            List<Product> cart = new List<Product>
            {
                new Product(
                imagePath: "../../images/item-cart-04.jpg",
                name: "Chaqueta",
                price: 360,
                quantity: 2
                ),
                new Product(
                imagePath: "../../images/item-cart-05.jpg",
                name: "Poleron",
                price: 160,
                quantity: 1
                )
            };
            ViewBag.Cart = cart;
            return View();
        }
    }
}