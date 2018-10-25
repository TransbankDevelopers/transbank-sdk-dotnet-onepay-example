using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Web.Mvc;
using OnepayMVCTest.Models;
using Transbank.Onepay.Model;
using System.Diagnostics;
using Transbank.Onepay.Enums;

namespace OnepayMVCTest.Controllers
{
    public class TransactionController : Controller
    {
        [HttpPost]
        public JsonResult Create(string channel)
        {
            var jsonResponse = new Hashtable();

            var channelType = ChannelType.Parse(channel);

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
                response = Transaction.Create(shoppingCart, channelType);
            }
            catch (Transbank.Onepay.Exceptions.TransbankException e)
            {
                jsonResponse.Add("error", e.Message);
                return Json(jsonResponse);
            }

            var camelCaseFormatter = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            jsonResponse.Add("occ", response.Occ);
            jsonResponse.Add("ott", response.Ott);
            jsonResponse.Add("externalUniqueNumber", response.ExternalUniqueNumber);
            jsonResponse.Add("qrCodeAsBase64", response.QrCodeAsBase64);
            jsonResponse.Add("issuedAt", response.IssuedAt);
            jsonResponse.Add("amount", shoppingCart.Total);

            return Json(jsonResponse);
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
            }
            catch (Transbank.Onepay.Exceptions.TransbankException e)
            {
                Debug.WriteLine(e.StackTrace);
                return RedirectToAction("Message", "Error", new {error = e.Message});
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
