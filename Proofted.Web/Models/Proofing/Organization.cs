namespace Proofted.Web.Models.Proofing
{
	using System.Collections.Generic;

	using T4TS;

	[TypeScriptInterface]
	public class Organization
	{
		#region Constructors and Destructors

		public Organization()
		{
			this.Users = new List<OrganizationUser>();
			this.Tags = new List<Tag>();
			this.Projects = new List<Project>();
			this.Invitations = new List<Invitation>();
		}

		#endregion

		#region Public Properties

		public string Name { get; set; }

		public int OrganizationId { get; set; }

		public List<Project> Projects { get; set; }

		public List<Tag> Tags { get; set; }

		public List<OrganizationUser> Users { get; set; }

		public List<Invitation> Invitations { get; set; }

		#endregion
	}
}