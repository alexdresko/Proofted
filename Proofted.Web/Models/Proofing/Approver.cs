﻿namespace Proofted.Web.Models.Proofing
{
	public class Approver
	{
		#region Public Properties

		public int ApproverId { get; set; }

		public bool Comments { get; set; }

		public Decision Decision { get; set; }

		public bool Opened { get; set; }

		public bool Sent { get; set; }

		public int UserId { get; set; }

		#endregion
	}
}