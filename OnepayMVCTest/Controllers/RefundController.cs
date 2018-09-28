using System;
using System.Web.Mvc;
using Transbank.Onepay.Model;

namespace OnePayMVCTest.Controllers
{
    public class RefundController : Controller
    {
        // GET
        public ActionResult Create(long amount, string occ, string externalUniqueNumber, string authorizationCode)
        {
            try
            {
                Refund.Create(amount, occ, externalUniqueNumber, authorizationCode);
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Message", "Error", new {error = e.Message});
            }
        }
    }
}
