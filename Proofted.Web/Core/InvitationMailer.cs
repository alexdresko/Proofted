namespace Proofted.Web.Core
{
	using System;

	using Proofted.Web.Models.Proofing;

	public class InvitationMailer
	{
		private readonly Invitation _invitation;

		private readonly ISmtpService _smtp;

		public InvitationMailer(Invitation invitation, ISmtpService smtp)
		{
			this._invitation = invitation;
			_smtp = smtp;
		}

		public void Mail()
		{
			_smtp.Mail(string.Empty, string.Empty, string.Empty);
		}

		public static void Mail(Invitation invitation)
		{
			var mailer = new InvitationMailer(invitation, new SmtpService());
			mailer.Mail();
		}
	}
}