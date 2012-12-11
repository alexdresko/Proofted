namespace Proofted.Web.DynamicData.PageTemplates
{
	using System;
	using System.Web.DynamicData;
	using System.Web.Routing;
	using System.Web.UI.WebControls;

	public partial class List : System.Web.UI.Page
    {
        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            this.table = DynamicDataRouteHandler.GetRequestMetaTable(this.Context);
            this.GridView1.SetMetaTable(this.table, this.table.GetColumnValuesFromRoute(this.Context));
            this.GridDataSource.EntityTypeFilter = this.table.EntityType.Name;

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = this.table.DisplayName;
            this.GridDataSource.Include = this.table.ForeignKeyColumnsNames;

            // Disable various options if the table is readonly
            if (this.table.IsReadOnly)
            {
                this.GridView1.Columns[0].Visible = false;
                this.InsertHyperLink.Visible = false;
                this.GridView1.EnablePersistedSelection = false;
            }
        }

        protected void Label_PreRender(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            DynamicFilter dynamicFilter = (DynamicFilter)label.FindControl("DynamicFilter");
            QueryableFilterUserControl fuc = dynamicFilter.FilterTemplate as QueryableFilterUserControl;
            if (fuc != null && fuc.FilterControl != null)
            {
                label.AssociatedControlID = fuc.FilterControl.GetUniqueIDRelativeTo(label);
            }
        }

        protected override void OnPreRenderComplete(EventArgs e)
        {
            RouteValueDictionary routeValues = new RouteValueDictionary(this.GridView1.GetDefaultValues());
            this.InsertHyperLink.NavigateUrl = this.table.GetActionPath(PageAction.Insert, routeValues);
            base.OnPreRenderComplete(e);
        }

        protected void DynamicFilter_FilterChanged(object sender, EventArgs e)
        {
            this.GridView1.PageIndex = 0;
        }

    }
}
