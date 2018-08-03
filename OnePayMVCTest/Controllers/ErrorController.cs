using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnePayMVCTest.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Message(string message)
        {
            ViewBag.Message = message;
            return View();
        }
    }
}