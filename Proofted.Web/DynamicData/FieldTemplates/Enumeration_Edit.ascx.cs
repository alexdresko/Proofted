namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Collections.Specialized;
	using System.Web.DynamicData;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class Enumeration_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private Type _enumType;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.DropDownList1.ToolTip = this.Column.Description;

            if (this.DropDownList1.Items.Count == 0)
            {
                if (this.Mode == DataBoundControlMode.Insert || !this.Column.IsRequired)
                {
                    this.DropDownList1.Items.Add(new ListItem("[Not Set]", String.Empty));
                }
                this.PopulateListControl(this.DropDownList1);
            }

            this.SetUpValidator(this.RequiredFieldValidator1);
            this.SetUpValidator(this.DynamicValidator1);
        }

        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            if (this.Mode == DataBoundControlMode.Edit && this.FieldValue != null)
            {
                string selectedValueString = this.GetSelectedValueString();
                ListItem item = this.DropDownList1.Items.FindByValue(selectedValueString);
                if (item != null)
                {
                    this.DropDownList1.SelectedValue = selectedValueString;
                }
            }
        }

        private Type EnumType
        {
            get
            {
                if (this._enumType == null)
                {
                    this._enumType = this.Column.GetEnumType();
                }
                return this._enumType;
            }
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            string value = this.DropDownList1.SelectedValue;
            if (value == String.Empty)
            {
                value = null;
            }
            dictionary[this.Column.Name] = this.ConvertEditedValue(value);
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
