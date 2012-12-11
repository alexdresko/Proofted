namespace Proofted.Web.Models.Proofing
{
	using System.Collections.Generic;

	public class ProjectFile
	{
		#region Public Properties

		public List<Approver> Approvers { get; set; }

		public int ProjectFileId { get; set; }

		public List<CommentThread> Threads { get; set; }

		#endregion
	}
}