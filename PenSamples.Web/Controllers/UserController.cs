namespace PenSamples.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;

    using PenSamples.Web.Data;
    using PenSamples.Web.Models;

    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        [Authorize]
        public async Task<ActionResult> Index()
        {
            using (var entities = new PenEntities())
            {
                var user = await entities.Users.FirstAsync(x => x.Name == User.Identity.Name);
                return View(new UserModel() { Name = user.Name, Status = user.Status });
            }
        }

        [HttpGet]
        [ValidateInput(false)]
        public async Task<ActionResult> ChangeStatus(string status = null)
        {
            using (var entities = new PenEntities())
            {
                var user = await entities
                    .Users
                    .Where(x => x.Name == User.Identity.Name)
                    .FirstAsync();

                if (status == null)
                {
                    return this.View("ChangeStatus", new StatusModel { Status = user.Status });
                };

                user.Status = status;
                await entities.SaveChangesAsync();

                return RedirectToAction("Index");
            }
        }
    }
}