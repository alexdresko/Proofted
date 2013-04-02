namespace Proofted.Web.Controllers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Transactions;
	using System.Web.Mvc;
	using System.Web.Security;

	using Proofted.Web.Filters;
	using Proofted.Web.Models.Security;

	[Authorize]
	[InitializeSimpleMembership]
	public partial class AccountController : Controller
	{
		// GET: /Account/Login
		#region Constructors and Destructors

		public AccountController()
			: this(new OAuthWebSecurityWrapper2(new OAuthWebSecurityWrapper()), new WebSecurityWrapper2(new WebSecurityWrapper())
				)
		{
		}

		public AccountController(IOAuthWebSecurity oAuthWebSecurity, IWebSecurity webSecurity)
		{
			_oAuthWebSecurity = oAuthWebSecurity;
			_webSecurity = webSecurity;
		}

		#endregion

		#region Enums

		public enum ManageMessageId
		{
			ChangePasswordSuccess, 

			SetPasswordSuccess, 

			RemoveLoginSuccess, 
		}

		#endregion

		#region Properties

		private static IOAuthWebSecurity _oAuthWebSecurity { get; set; }

		private static IWebSecurity _webSecurity { get; set; }

		#endregion

		// POST: /Account/Disassociate
		#region Public Methods and Operators

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Disassociate(string provider, string providerUserId)
		{
			var ownerAccount = _oAuthWebSecurity.GetUserName(provider, providerUserId);
			ManageMessageId? message = null;

			// Only disassociate the account if the currently logged in user is the owner
			if (ownerAccount == this.User.Identity.Name)
			{
				// Use a transaction to prevent the user from deleting their last login credential
				using (
					var scope = new TransactionScope(
						TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable }))
				{
					var hasLocalAccount = _oAuthWebSecurity.HasLocalAccount(_webSecurity.GetUserId(this.User.Identity.Name));
					if (hasLocalAccount || _oAuthWebSecurity.GetAccountsFromUserName(this.User.Identity.Name).Count > 1)
					{
						_oAuthWebSecurity.DeleteAccount(provider, providerUserId);
						scope.Complete();
						message = ManageMessageId.RemoveLoginSuccess;
					}
				}
			}

			return this.RedirectToAction("Manage", new { Message = message });
		}

		// GET: /Account/Manage

		// POST: /Account/ExternalLogin
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public virtual ActionResult ExternalLogin(string provider, string returnUrl)
		{
			return new ExternalLoginResult(provider, this.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
		}

		// GET: /Account/ExternalLoginCallback
		[AllowAnonymous]
		public virtual ActionResult ExternalLoginCallback(string returnUrl)
		{
			var result =
				_oAuthWebSecurity.VerifyAuthentication(this.Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
			if (!result.IsSuccessful)
			{
				return this.RedirectToAction("ExternalLoginFailure");
			}

			if (_oAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false))
			{
				return this.RedirectToLocal(returnUrl);
			}

			if (this.User.Identity.IsAuthenticated)
			{
				// If the current user is logged in add the new account
				_oAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, this.User.Identity.Name);
				return this.RedirectToLocal(returnUrl);
			}
			else
			{
				// User is new, ask for their desired membership name
				var loginData = _oAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
				this.ViewBag.ProviderDisplayName = _oAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
				this.ViewBag.ReturnUrl = returnUrl;
				return this.View(
					"ExternalLoginConfirmation", 
					new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
			}
		}

		// POST: /Account/ExternalLoginConfirmation
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public virtual ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl)
		{
			string provider = null;
			string providerUserId = null;

			if (this.User.Identity.IsAuthenticated
			    || !_oAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId))
			{
				return this.RedirectToAction("Manage");
			}

			if (this.ModelState.IsValid)
			{
				// Insert a new user into the database
				using (var db = new UserDbContext())
				{
					var user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());

					// Check if user already exists
					if (user == null)
					{
						// Insert name into the profile table
						db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
						db.SaveChanges();

						_oAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
						_oAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

						return this.RedirectToLocal(returnUrl);
					}
					else
					{
						this.ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
					}
				}
			}

			this.ViewBag.ProviderDisplayName = _oAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
			this.ViewBag.ReturnUrl = returnUrl;
			return View(model);
		}

		// GET: /Account/ExternalLoginFailure
		[AllowAnonymous]
		public virtual ActionResult ExternalLoginFailure()
		{
			return this.View();
		}

		[AllowAnonymous]
		[ChildActionOnly]
		public virtual ActionResult ExternalLoginsList(string returnUrl)
		{
			this.ViewBag.ReturnUrl = returnUrl;
			return this.PartialView("_ExternalLoginsListPartial", _oAuthWebSecurity.RegisteredClientData);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult LogOff()
		{
			_webSecurity.Logout();

			return this.RedirectToAction("Index", "Home");
		}

		[AllowAnonymous]
		public virtual ActionResult Login(string returnUrl)
		{
			this.ViewBag.ReturnUrl = returnUrl;
			return this.View();
		}

		// POST: /Account/Login
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Login(LoginModel model, string returnUrl)
		{
			if (this.ModelState.IsValid && _webSecurity.Login(model.UserName, model.Password, persistCookie: model.RememberMe))
			{
				return this.RedirectToLocal(returnUrl);
			}

			// If we got this far, something failed, redisplay form
			this.ModelState.AddModelError(string.Empty, "The user name or password provided is incorrect.");
			return View(model);
		}

		public virtual ActionResult Manage(ManageMessageId? message)
		{
			this.ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess
				                             ? "Your password has been changed."
				                             : message == ManageMessageId.SetPasswordSuccess
					                               ? "Your password has been set."
					                               : message == ManageMessageId.RemoveLoginSuccess
						                                 ? "The external login was removed."
						                                 : string.Empty;
			this.ViewBag.HasLocalPassword = _oAuthWebSecurity.HasLocalAccount(_webSecurity.GetUserId(this.User.Identity.Name));
			this.ViewBag.ReturnUrl = this.Url.Action("Manage");
			return this.View();
		}

		// POST: /Account/Manage
		[HttpPost]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Manage(LocalPasswordModel model)
		{
			var hasLocalAccount = _oAuthWebSecurity.HasLocalAccount(_webSecurity.GetUserId(this.User.Identity.Name));
			this.ViewBag.HasLocalPassword = hasLocalAccount;
			this.ViewBag.ReturnUrl = this.Url.Action("Manage");
			if (hasLocalAccount)
			{
				if (this.ModelState.IsValid)
				{
					// ChangePassword will throw an exception rather than return false in certain failure scenarios.
					bool changePasswordSucceeded;
					try
					{
						changePasswordSucceeded = _webSecurity.ChangePassword(
							this.User.Identity.Name, model.OldPassword, model.NewPassword);
					}
					catch (Exception)
					{
						changePasswordSucceeded = false;
					}

					if (changePasswordSucceeded)
					{
						return this.RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
					}
					else
					{
						this.ModelState.AddModelError(string.Empty, "The current password is incorrect or the new password is invalid.");
					}
				}
			}
			else
			{
				// User does not have a local password so remove any validation errors caused by a missing
				// OldPassword field
				var state = this.ModelState["OldPassword"];
				if (state != null)
				{
					state.Errors.Clear();
				}

				if (this.ModelState.IsValid)
				{
					try
					{
						_webSecurity.CreateAccount(this.User.Identity.Name, model.NewPassword);
						return this.RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
					}
					catch (Exception e)
					{
						this.ModelState.AddModelError(string.Empty, e);
					}
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[AllowAnonymous]
		public virtual ActionResult Register()
		{
			return this.View();
		}

		// POST: /Account/Register
		[HttpPost]
		[AllowAnonymous]
		[ValidateAntiForgeryToken]
		public virtual ActionResult Register(RegisterModel model)
		{
			if (this.ModelState.IsValid)
			{
				// Attempt to register the user
				try
				{
					_webSecurity.CreateUserAndAccount(model.UserName, model.Password);
					_webSecurity.Login(model.UserName, model.Password);
					return this.RedirectToAction("Index", "Home");
				}
				catch (MembershipCreateUserException e)
				{
					this.ModelState.AddModelError(string.Empty, ErrorCodeToString(e.StatusCode));
				}
			}

			// If we got this far, something failed, redisplay form
			return View(model);
		}

		[ChildActionOnly]
		public virtual ActionResult RemoveExternalLogins()
		{
			var accounts = _oAuthWebSecurity.GetAccountsFromUserName(this.User.Identity.Name);
			var externalLogins = new List<ExternalLogin>();
			foreach (var account in accounts)
			{
				var clientData = _oAuthWebSecurity.GetOAuthClientData(account.Provider);

				externalLogins.Add(
					new ExternalLogin
						{
							Provider = account.Provider, 
							ProviderDisplayName = clientData.DisplayName, 
							ProviderUserId = account.ProviderUserId, 
						});
			}

			this.ViewBag.ShowRemoveButton = externalLogins.Count > 1
			                                || _oAuthWebSecurity.HasLocalAccount(_webSecurity.GetUserId(this.User.Identity.Name));
			return this.PartialView("_RemoveExternalLoginsPartial", externalLogins);
		}

		#endregion

		#region Methods

		private static string ErrorCodeToString(MembershipCreateStatus createStatus)
		{
			// See http://go.microsoft.com/fwlink/?LinkID=177550 for
			// a full list of status codes.
			switch (createStatus)
			{
				case MembershipCreateStatus.DuplicateUserName:
					return "User name already exists. Please enter a different user name.";

				case MembershipCreateStatus.DuplicateEmail:
					return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

				case MembershipCreateStatus.InvalidPassword:
					return "The password provided is invalid. Please enter a valid password value.";

				case MembershipCreateStatus.InvalidEmail:
					return "The e-mail address provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidAnswer:
					return "The password retrieval answer provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidQuestion:
					return "The password retrieval question provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.InvalidUserName:
					return "The user name provided is invalid. Please check the value and try again.";

				case MembershipCreateStatus.ProviderError:
					return
						"The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				case MembershipCreateStatus.UserRejected:
					return
						"The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

				default:
					return
						"An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
			}
		}

		private ActionResult RedirectToLocal(string returnUrl)
		{
			if (this.Url.IsLocalUrl(returnUrl))
			{
				return this.Redirect(returnUrl);
			}
			else
			{
				return this.RedirectToAction("Index", "Home");
			}
		}

		#endregion

		internal class ExternalLoginResult : ActionResult
		{
			#region Constructors and Destructors

			public ExternalLoginResult(string provider, string returnUrl)
			{
				this.Provider = provider;
				this.ReturnUrl = returnUrl;
			}

			#endregion

			#region Public Properties

			public string Provider { get; private set; }

			public string ReturnUrl { get; private set; }

			#endregion

			#region Public Methods and Operators

			public override void ExecuteResult(ControllerContext context)
			{
				_oAuthWebSecurity.RequestAuthentication(this.Provider, this.ReturnUrl);
			}

			#endregion
		}
	}
}