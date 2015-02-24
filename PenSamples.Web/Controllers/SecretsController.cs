namespace PenSamples.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using PenSamples.Web.Data;
    using PenSamples.Web.Models;

    [Authorize]
    public class SecretsController : Controller
    {
        // GET: Secrets
        public async Task<ActionResult> List()
        {
            var username = User.Identity.Name;

            using (var entities = new PenEntities())
            {
                var secrets = await entities
                    .Users
                    .Where(x => x.Name == username)
                    .SelectMany(x => x.Secrets)
                    .Select(x => new SecretModel() { Description = x.Description, Text = x.Text, User = x.User.Name })
                    .ToListAsync();

                return View(new SecretsListModel()
                                {
                                    User = username,
                                    Secrets = secrets
                                });
            }
        }


        // GET: Secrets/1
        public async Task<ActionResult> Details(int secretId)
        {
            var username = User.Identity.Name;
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
                                         Description = secret.Description,
                                         Text = secret.Text,
                                         User = secret.User.Name
                                     });
            }
        }
    }
}