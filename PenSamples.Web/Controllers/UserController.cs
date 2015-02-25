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

        [HttpGet]
        [ValidateInput(false)]
        public ActionResult ChangeStatus(string status = null)
        {
            if (status == null)
            {
                return this.View();
            }

            return RedirectToAction("Index");
        }
    }
}