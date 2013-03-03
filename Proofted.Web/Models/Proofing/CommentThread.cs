﻿namespace Proofted.Web.Models.Proofing
{
	using System.Collections.Generic;

	using Proofted.Web.Models.Security;

	public class CommentThread
	{
		#region Public Properties

		public int CommentThreadId { get; set; }

		public List<Comment> Comments { get; set; }

		//public ThreadStatus ThreadStatus { get; set; }
		public int ThreadStatus { get; set; }

		public int UserId { get; set; }

		public int ProjectFileId { get; set; }

		public ProjectFile ProjectFile { get; set; }

		#endregion
	}
}