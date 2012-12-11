namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System.Web.UI;

	public partial class DateTimeField : System.Web.DynamicData.FieldTemplateUserControl
    {
        public override Control DataControl
        {
            get
            {
                return this.Literal1;
            }
        }

    }
}
