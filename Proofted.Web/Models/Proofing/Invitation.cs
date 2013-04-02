namespace Proofted.Web.Models.Proofing
{
	using System;
	using System.ComponentModel.DataAnnotations;

	public class Invitation
	{
		public int InvitationId { get; set; }

		public int OrganizationId { get; set; }

		public Organization Organization { get; set; }

		[MinLength(6, ErrorMessage = "Sorry, gotta make that sucker longer.")]
		public string EmailAddress { get; set; }

		public string Guid { get; set; }

		public bool Used { get; set; }
	}

	public class TestValidatorAttribute : ValidationAttribute
	{
		public override bool IsValid(object value)
		{
			var stringValue = value as string;
			if (!string.IsNullOrWhiteSpace(stringValue))
			{
				return !stringValue.ToLower().Contains("2");
			}

			return true;
		}
	}
}