namespace Proofted.Web.DynamicData.Filters
{
	using System;
	using System.Collections;
	using System.Linq;
	using System.Web.DynamicData;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class ForeignKeyFilter : System.Web.DynamicData.QueryableFilterUserControl
    {
        private const string NullValueString = "[null]";
        private new MetaForeignKeyColumn Column
        {
            get
            {
                return (MetaForeignKeyColumn)base.Column;
            }
        }

        public override Control FilterControl
        {
            get
            {
                return this.DropDownList1;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
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

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.OnFilterChanged();
        }

        public override IQueryable GetQueryable(IQueryable source)
        {
            string selectedValue = this.DropDownList1.SelectedValue;
            if (String.IsNullOrEmpty(selectedValue))
            {
                return source;
            }

            if (selectedValue == NullValueString)
            {
                return ApplyEqualityFilter(source, this.Column.Name, null);
            }

            IDictionary dict = new Hashtable();
            this.Column.ExtractForeignKey(dict, selectedValue);
            foreach (DictionaryEntry entry in dict)
            {
                string key = (string)entry.Key;
                if (this.DefaultValues != null)
                {
                    this.DefaultValues[key] = entry.Value;
                }
                source = ApplyEqualityFilter(source, this.Column.GetFilterExpression(key), entry.Value);
            }
            return source;
        }

    }
}
