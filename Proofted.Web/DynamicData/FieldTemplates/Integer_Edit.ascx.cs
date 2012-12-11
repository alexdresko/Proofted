namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Collections.Specialized;
	using System.Web.UI;

	public partial class Integer_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TextBox1.ToolTip = this.Column.Description;

            this.SetUpValidator(this.RequiredFieldValidator1);
            this.SetUpValidator(this.CompareValidator1);
            this.SetUpValidator(this.RegularExpressionValidator1);
            this.SetUpValidator(this.RangeValidator1);
            this.SetUpValidator(this.DynamicValidator1);
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[this.Column.Name] = this.ConvertEditedValue(this.TextBox1.Text);
        }

        public override Control DataControl
        {
            get
            {
                return this.TextBox1;
            }
        }

    }
}
