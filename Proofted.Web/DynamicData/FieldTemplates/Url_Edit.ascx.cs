namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Collections.Specialized;
	using System.Web.UI;

	public partial class Url_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Column.MaxLength < 20)
            {
                this.TextBox1.Columns = this.Column.MaxLength;
            }
            this.TextBox1.ToolTip = this.Column.Description;

            this.SetUpValidator(this.RequiredFieldValidator1);
            this.SetUpValidator(this.RegularExpressionValidator1);
            this.SetUpValidator(this.DynamicValidator1);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);
            if (this.Column.MaxLength > 0)
            {
                this.TextBox1.MaxLength = Math.Max(this.FieldValueEditString.Length, this.Column.MaxLength);
            }
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