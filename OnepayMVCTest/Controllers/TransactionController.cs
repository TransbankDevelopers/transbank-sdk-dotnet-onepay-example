using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Mvc;
using OnepayMVCTest.Models;
using Transbank.Onepay.Model;
using System.Diagnostics;

namespace OnepayMVCTest.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public ActionResult Create()
        {
            var cart = GetCart();
            ShoppingCart shoppingCart = new ShoppingCart();
            foreach (Product p in cart)
                shoppingCart.Add(new Item(
                    description: p.Name,
                    quantity: p.Quantity,
                    amount: p.Price,
                    additionalData: null,
                    expire: 10
                    ));
            TransactionCreateResponse response;
            try
            {
                response = Transaction.Create(shoppingCart);
            } catch (Transbank.Onepay.Exceptions.TransbankException e)
            {
                return RedirectToAction("Error", "Message", new { error = e.Message });
            }

            var camelCaseFormatter = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var toJson = new Hashtable();
            toJson.Add("occ", response.Occ);
            toJson.Add("ott", response.Ott);
            toJson.Add("externalUniqueNumber", response.ExternalUniqueNumber);
            toJson.Add("qrCodeAsBase64", response.QrCodeAsBase64);
            toJson.Add("issuedAt", response.IssuedAt);
            toJson.Add("amount", shoppingCart.Total);
            
            var calCaseJson = JsonConvert.SerializeObject(toJson, camelCaseFormatter);
            return Json(calCaseJson);
        }

        [HttpGet]
        public ActionResult Commit(string occ, string externalUniqueNumber, string status)
        {
            if (null != status && !status.Equals("PRE_AUTHORIZED", StringComparison.InvariantCultureIgnoreCase))
            {
                ViewBag.Occ = occ;
                ViewBag.ExternalUniqueNumber = externalUniqueNumber;
                ViewBag.Status = status;

                return View("CommitError");
            }
            
            try
            {
                TransactionCommitResponse response = Transaction.Commit(occ, externalUniqueNumber);

                ViewBag.ExternalUniqueNumber = externalUniqueNumber;
                ViewBag.Transaction = response;
                return View();
            } catch (Transbank.Onepay.Exceptions.TransbankException e)
            {
                Debug.WriteLine(e.StackTrace);
                return RedirectToAction("Error","Message", new { error = e.Message });
            }
        }

        private List<Product> GetCart()
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
            return cart;
        }
    }
}
