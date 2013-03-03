namespace Proofted.Web.Core
{
	public interface ISmtpService
	{
		void Mail(string recipient, string subject, string body);
	}
}