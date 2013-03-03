namespace Proofted.Web.Models.Proofing
{
	public class OrganizationUser
	{
		public int OrganizationUserId { get; set; }

		public string UserId { get; set; }

		public int OrganizationId { get; set; }

		public Organization Organization { get; set; }
	}
}