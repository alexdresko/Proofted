namespace Proofted.Web.DynamicData.PageTemplates
{
	using System;
	using System.Web.DynamicData;
	using System.Web.UI.WebControls;

	public partial class Details : System.Web.UI.Page
    {
        protected MetaTable table;

        protected void Page_Init(object sender, EventArgs e)
        {
            this.table = DynamicDataRouteHandler.GetRequestMetaTable(this.Context);
            this.FormView1.SetMetaTable(this.table);
            this.DetailsDataSource.EntityTypeFilter = this.table.EntityType.Name;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = this.table.DisplayName;
            this.DetailsDataSource.Include = this.table.ForeignKeyColumnsNames;
        }

        protected void FormView1_ItemDeleted(object sender, FormViewDeletedEventArgs e)
        {
            if (e.Exception == null || e.ExceptionHandled)
            {
                this.Response.Redirect(this.table.ListActionPath);
            }
        }

    }
}
