namespace Proofted.Web.App_Start
{
	using Proofted.Web.Core;
	using Proofted.Web.Models.Security;

	public static class AuthConfig
	{
		#region Public Methods and Operators

		public static void RegisterAuth()
		{
			// To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
			// you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

			// OAuthWebSecurity.RegisterMicrosoftClient(
			// clientId: "",
			// clientSecret: "");

			// OAuthWebSecurity.RegisterTwitterClient(
			// consumerKey: "",
			// consumerSecret: "");
			var reg = new OAuthRegistrar(new OAuthWebSecurityWrapper());

			reg.RegisterFacebookClient();

			reg.RegisterGoogleClient();
		}

		#endregion
	}
}