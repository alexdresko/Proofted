namespace Proofted.Web.DynamicData.Filters
{
	using System;
	using System.Linq;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class EnumerationFilter : System.Web.DynamicData.QueryableFilterUserControl
    {
        private const string NullValueString = "[null]";
        public override Control FilterControl
        {
            get
            {
                return this.DropDownList1;
            }
        }

        public void Page_Init(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                if (!this.Column.IsRequired)
                {
                    this.DropDownList1.Items.Add(new ListItem("[Not Set]", NullValueString));
                }
                this.PopulateListControl(this.DropDownList1);
                // Set the initial value if there is one
                string initialValue = this.DefaultValue;
                if (!String.IsNullOrEmpty(initialValue))
                {
                    this.DropDownList1.SelectedValue = initialValue;
                }
            }
        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            string selectedValue = this.DropDownList1.SelectedValue;
            if (String.IsNullOrEmpty(selectedValue))
            {
                return source;
            }

            object value = selectedValue;
            if (selectedValue == NullValueString)
            {
                value = null;
            }
            if (this.DefaultValues != null)
            {
                this.DefaultValues[this.Column.Name] = value;
            }
            return ApplyEqualityFilter(source, this.Column.Name, value);
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnFilterChanged();
        }

    }
}
