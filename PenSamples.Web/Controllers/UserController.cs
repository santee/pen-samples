namespace PenSamples.Web.Controllers
{
    using System.Web.Mvc;

    using PenSamples.Web.Models;

    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        [Authorize]
        public ActionResult Index()
        {
            return View(new UserModel()
                            {
                                Name = User.Identity.Name
                            });
        }
    }
}