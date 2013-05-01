namespace Proofted.Web.Models.Proofing
{
	using System;

	public class Log
	{
		#region Constructors and Destructors

		public Log()
		{
			this.CreatedDateTime = DateTime.UtcNow;
		}

		#endregion

		#region Public Properties

		public DateTime CreatedDateTime { get; set; }

		public string Details { get; set; }

		public int LogId { get; set; }

		public LogLevel LogLevel { get; set; }

		public LogType LogType { get; set; }

		public string Message { get; set; }

		public int MessageId { get; set; }

		public string Module { get; set; }

		public string StackTrace { get; set; }

		public string User { get; set; }

		public int FileNo { get; set; }

		public string Source { get; set; }

		#endregion
	}
}