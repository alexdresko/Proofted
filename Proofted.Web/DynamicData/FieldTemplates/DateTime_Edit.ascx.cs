namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Collections.Specialized;
	using System.ComponentModel.DataAnnotations;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class DateTime_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private static DataTypeAttribute DefaultDateAttribute = new DataTypeAttribute(DataType.DateTime);
        protected void Page_Load(object sender, EventArgs e)
        {
            this.TextBox1.ToolTip = this.Column.Description;

            this.SetUpValidator(this.RequiredFieldValidator1);
            this.SetUpValidator(this.RegularExpressionValidator1);
            this.SetUpValidator(this.DynamicValidator1);
            this.SetUpCustomValidator(this.DateValidator);
        }

        private void SetUpCustomValidator(CustomValidator validator)
        {
            if (this.Column.DataTypeAttribute != null)
            {
                switch (this.Column.DataTypeAttribute.DataType)
                {
                    case DataType.Date:
                    case DataType.DateTime:
                    case DataType.Time:
                        validator.Enabled = true;
                        this.DateValidator.ErrorMessage = HttpUtility.HtmlEncode(this.Column.DataTypeAttribute.FormatErrorMessage(this.Column.DisplayName));
                        break;
                }
            }
            else if (this.Column.ColumnType.Equals(typeof(DateTime)))
            {
                validator.Enabled = true;
                this.DateValidator.ErrorMessage = HttpUtility.HtmlEncode(DefaultDateAttribute.FormatErrorMessage(this.Column.DisplayName));
            }
        }

        protected void DateValidator_ServerValidate(object source, ServerValidateEventArgs args)
        {
            DateTime dummyResult;
            args.IsValid = DateTime.TryParse(args.Value, out dummyResult);
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
