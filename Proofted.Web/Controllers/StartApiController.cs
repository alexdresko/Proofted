namespace Proofted.Web.Controllers
{
	using System.Linq;
	using System.Web.Http;

	using Breeze.WebApi;

	using Newtonsoft.Json.Linq;

	using Proofted.Web.Models;
	using Proofted.Web.Models.Proofing;

	[BreezeController]
	public class StartApiController : ApiController
	{
		#region Static Fields

		#endregion

		// static DateTime lastRefresh = DateTime.MinValue; // will clear when server starts
		#region Fields

		private readonly ProoftedEfContextProvider _contextProvider = new ProoftedEfContextProvider();

		#endregion

		#region Constructors and Destructors


		#endregion

		// ~/api/todos/Metadata 
		#region Public Methods and Operators

		[HttpGet]
		public string Metadata()
		{
			return this._contextProvider.Metadata();
		}


		[HttpPost]
		public SaveResult SaveChanges(JObject saveBundle)
		{
			return this._contextProvider.SaveChanges(saveBundle);
		}

		[HttpGet]
		public IQueryable<Organization> Organizations()
		{
			return
				this._contextProvider.Context.Organizations.Where(
					p => p.Users.Any(f => f.UserId == System.Web.HttpContext.Current.User.Identity.Name));
		}

		#endregion

		#region Methods


		#endregion
	}
}