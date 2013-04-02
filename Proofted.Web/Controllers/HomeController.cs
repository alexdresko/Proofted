namespace Proofted.Web.Controllers
{
	using System.Web.Mvc;
	[AllowAnonymous]
	public partial class HomeController : Controller
	{
		#region Public Methods and Operators

		public virtual ActionResult About()
		{
			return this.View();
		}

		public virtual ActionResult Contact()
		{
			this.ViewBag.Message = "Your contact page.";

			return this.View();
		}

		public virtual ActionResult Index()
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