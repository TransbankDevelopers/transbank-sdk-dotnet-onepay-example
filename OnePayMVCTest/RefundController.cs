using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Onepay.Model;
using Transbank.Onepay.Exceptions;
using System.Diagnostics;

namespace OnePayMVCTest
{
    public class RefundController : Controller
    {
        [HttpGet]
        public ActionResult Create(long amount, string occ, string externalUniqueNumber, string authorizationCode)
        {
            try
            {
                RefundCreateResponse refundResponse = Refund.Create(amount, occ, externalUniqueNumber, authorizationCode);
                ViewBag.Refund = refundResponse;
                return View();
            }
            catch (TransbankException e)
            {
                Debug.WriteLine(e.StackTrace);
                return RedirectToAction("Error", "Message", new { error = e.Message });
            }
        }
    }
}