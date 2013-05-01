namespace Proofted.Web.Models.Proofing
{
	using System.ComponentModel.DataAnnotations;

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