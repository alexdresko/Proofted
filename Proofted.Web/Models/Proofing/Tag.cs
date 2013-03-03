namespace Proofted.Web.Models.Proofing
{
	using System.Collections.Generic;

	public class Tag
	{
		#region Public Properties

		public List<Project> Projects { get; set; }

		public int TagId { get; set; }

		public int OrganizationId { get; set; }

		public Organization Organization { get; set; }

		#endregion
	}
}