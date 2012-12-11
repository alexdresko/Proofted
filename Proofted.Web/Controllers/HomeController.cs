namespace Proofted.Web.Controllers
{
	using System.Web.Mvc;
	[AllowAnonymous]
	public class HomeController : Controller
	{
		#region Public Methods and Operators

		public ActionResult About()
		{
			return this.View();
		}

		public ActionResult Contact()
		{
			this.ViewBag.Message = "Your contact page.";

			return this.View();
		}

		public ActionResult Index()
		{
			if (Request.IsAuthenticated)
			{
				return this.RedirectToAction("Index", "Start");
			}

			return this.View();
		}

		#endregion
	}
}