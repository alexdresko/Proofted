namespace Proofted.Web.Models.Proofing
{
	using System.Collections.Generic;

	public class Project
	{
		#region Public Properties

		public string Description { get; set; }

		public string Name { get; set; }

		public List<ProjectFile> ProjectFiles { get; set; }

		public int ProjectId { get; set; }

		public List<Tag> Tags { get; set; }

		public int OrganizationId { get; set; }

		public Organization Organization { get; set; }

		#endregion
	}
}