using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proofted.Web.Controllers
{
	using System.Diagnostics;

	using Proofted.Web.Models.Proofing;

	public partial class LoggingController : Controller
    {
        //
        // GET: /Logging/

		public virtual void GlobalJavascriptError(string eventOrMessage, string source, int fileno)
		{
			ProoftedDbContext.Borrow(
				context =>
					{
						context.Logs.Add(
							new Log()
								{
									Message = eventOrMessage,
									Source = source,
									FileNo = fileno,
									LogLevel = LogLevel.Global,
									LogType = LogType.JavaScript,
									User = this.User.Identity.Name
								});
						context.SaveChanges();
					});
		}

    }
}
