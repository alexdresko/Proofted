namespace Proofted.Web.Models.Proofing
{
	public class Comment
	{
		#region Public Properties

		public int CommentId { get; set; }

		public int CommentThreadId { get; set; }

		public CommentThread CommentThread { get; set; }

		#endregion
	}
}