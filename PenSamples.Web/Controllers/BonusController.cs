namespace PenSamples.Web.Controllers
{
    using System.Dynamic;
    using System.Web.Mvc;

    public class BonusController : Controller
    {
        public JsonResult Get()
        {
            dynamic obj1 = new ExpandoObject();
            obj1.password = "qwerty";

            return new JsonResult() { Data = new[] { obj1 }, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public JsonResult GetAsObject()
        {
            dynamic obj1 = new ExpandoObject();
            obj1.password = "qwerty";

            dynamic wrapper = new ExpandoObject();
            wrapper.d = new[] { obj1 };

            return new JsonResult() { Data = wrapper, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}