using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PenSamples.Web.Controllers
{
    using PenSamples.Web.Data;
    using PenSamples.Web.Models;

    [Authorize]
    public class SecretsController : Controller
    {
        // GET: Secrets
        public ActionResult List()
        {
            var username = User.Identity.Name;

            using (var entities = new PenEntities())
            {
                var secrets =
                    entities.Secrets.Select(
                        x => new SecretModel() { Description = x.Description, Text = x.Text, User = username });

                return View(new SecretsListModel()
                                {
                                    User = username,
                                    Secrets = secrets
                                });
            }
        }
    }
}