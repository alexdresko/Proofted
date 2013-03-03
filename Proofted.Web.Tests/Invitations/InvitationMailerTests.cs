namespace Proofted.Web.Tests.Invitations
{
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using NSubstitute;

	using Ploeh.AutoFixture;
	using Ploeh.AutoFixture.AutoNSubstitute;

	using Proofted.Web.Core;
	using Proofted.Web.Models.Proofing;

	[TestClass]
	public class InvitationMailerTests
	{
		#region Fields

		private IFixture _fixture;

		#endregion

		#region Public Methods and Operators

		[TestMethod]
		public void Mail_GivenValidInvitation_Mails()
		{
			this.CreateValidInvitation();

			var smtp = this._fixture.Freeze<ISmtpService>();
			var mailer = this._fixture.Create<InvitationMailer>();

			mailer.Mail();

			smtp.Received().Mail(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
		}



		[TestInitialize]
		public void TestInitialize()
		{
			this._fixture = GetFixture();
		}

		#endregion

		#region Methods

		private static IFixture GetFixture()
		{
			var fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
			fixture.Behaviors.Add(new OmitOnRecursionBehavior());
			return fixture;
		}

		private void CreateValidInvitation()
		{
			var invitation = this._fixture.Build<Invitation>().With(p => p.EmailAddress, "me@alexdresko.com").CreateAnonymous();
		}

		#endregion
	}
}