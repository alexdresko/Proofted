namespace Proofted.Web.Models.Proofing
{
	public class Invitation
	{
		public int InvitationId { get; set; }

		public int OrganizationId { get; set; }

		public Organization Organization { get; set; }

		public string EmailAddress { get; set; }

		public string Guid { get; set; }

		public bool Used { get; set; }
	}
}