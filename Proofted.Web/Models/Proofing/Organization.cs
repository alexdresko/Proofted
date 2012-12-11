namespace Proofted.Web.Models.Proofing
{
	using System.Collections.Generic;

	public class Organization
	{
		#region Public Properties

		public string Name { get; set; }

		public int OrganizationId { get; set; }

		public List<Project> Projects { get; set; }

		#endregion
	}
}