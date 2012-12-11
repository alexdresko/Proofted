namespace Proofted.Web.DynamicData.Filters
{
	using System;
	using System.Linq;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class BooleanFilter : System.Web.DynamicData.QueryableFilterUserControl
    {
        private const string NullValueString = "[null]";
        public override Control FilterControl
        {
            get
            {
                return this.DropDownList1;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!this.Column.ColumnType.Equals(typeof(bool)))
            {
                throw new InvalidOperationException(String.Format("A boolean filter was loaded for column '{0}' but the column has an incompatible type '{1}'.", this.Column.Name, this.Column.ColumnType));
            }

            if (!this.Page.IsPostBack)
            {
                this.DropDownList1.Items.Add(new ListItem("All", String.Empty));
                if (!this.Column.IsRequired)
                {
                    this.DropDownList1.Items.Add(new ListItem("[Not Set]", NullValueString));
                }
                this.DropDownList1.Items.Add(new ListItem("True", Boolean.TrueString));
                this.DropDownList1.Items.Add(new ListItem("False", Boolean.FalseString));
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
