﻿namespace PenSamples.Web.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Helpers;
    using System.Web.Mvc;

    using PenSamples.Web.Data;
    using PenSamples.Web.Models;

    [Authorize]
    public class SecretsController : Controller
    {
        // GET: Secrets
        public async Task<ActionResult> List(string filter = "")
        {
            var username = User.Identity.Name;

            var query = @"SELECT s.SecretId as SecretId, Description, Text, Name as [User] FROM Users u
                          JOIN Secrets s ON s.UserId = u.UserId
                          WHERE Name = '{0}'";

            var completeQuery = string.Format(query, username);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                completeQuery += string.Format(" AND Description LIKE '%{0}%'", filter);
            }

            using (var entities = new PenEntities())
            {
                var secrets = await entities.Database.SqlQuery<SecretModel>(completeQuery).ToListAsync();

            //    var secrets = await entities
            //        .Users
            //        .Where(x => x.Name == username)
            //        .SelectMany(x => x.Secrets)
            //        .Select(x => new SecretModel() { SecretId = x.SecretId, Description = x.Description, Text = x.Text, User = x.User.Name })
            //        .ToListAsync();

                return View(new SecretsListModel()
                                {
                                    User = username,
                                    Secrets = secrets,
                                    Filter = filter
                                });
            }
        }


        // GET: Secrets/1
        public async Task<ActionResult> Details(int secretId)
        {
            //NOTE: We do not check user
            using (var entities = new PenEntities())
            {
                var secret = await entities.Secrets.FirstOrDefaultAsync(x => x.SecretId == secretId);

                if (secret == null)
                {
                    throw new HttpException(404, "Secret not found");
                }

                return this.View(new SecretModel
                                     {
                                         SecretId = secret.SecretId,
                                         Description = secret.Description,
                                         Text = secret.Text,
                                         User = secret.User.Name
                                     });
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return this.View(new SecretModel());
        }

        [HttpPost]
        public async Task<ActionResult> Create(SecretModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            using (var entities = new PenEntities())
            {
                var user = await entities.Users.FirstAsync(x => x.Name == User.Identity.Name);
                user.Secrets.Add(new Secret()
                                     {
                                         Description = model.Description,
                                         Text = model.Text
                                     });
                await entities.SaveChangesAsync();
            }

            return RedirectToAction("List");
        }
    }
}