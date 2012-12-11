namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Collections.Specialized;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class ForeignKey_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.DropDownList1.Items.Count == 0)
            {
                if (this.Mode == DataBoundControlMode.Insert || !this.Column.IsRequired)
                {
                    this.DropDownList1.Items.Add(new ListItem("[Not Set]", ""));
                }
                this.PopulateListControl(this.DropDownList1);
            }

            this.SetUpValidator(this.RequiredFieldValidator1);
            this.SetUpValidator(this.DynamicValidator1);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            string selectedValueString = this.GetSelectedValueString();
            ListItem item = this.DropDownList1.Items.FindByValue(selectedValueString);
            if (item != null)
            {
                this.DropDownList1.SelectedValue = selectedValueString;
            }

        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            // If it's an empty string, change it to null
            string value = this.DropDownList1.SelectedValue;
            if (String.IsNullOrEmpty(value))
            {
                value = null;
            }

            this.ExtractForeignKey(dictionary, value);
        }

        public override Control DataControl
        {
            get
            {
                return this.DropDownList1;
            }
        }

    }
}
