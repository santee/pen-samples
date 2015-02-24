using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PenSamples.Web.Controllers
{
    public class SecretsController : Controller
    {
        // GET: Secrets
        public ActionResult Index()
        {
            return View();
        }
    }
}