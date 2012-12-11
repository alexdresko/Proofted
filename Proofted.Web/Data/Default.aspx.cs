namespace Proofted.Web.Data
{
	using System;

	using Proofted.Web;

	public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Collections.IList visibleTables = MvcApplication.DefaultModel.VisibleTables;
            if (visibleTables.Count == 0)
            {
                throw new InvalidOperationException("There are no accessible tables. Make sure that at least one data model is registered in Global.asax and scaffolding is enabled or implement custom pages.");
            }
            this.Menu1.DataSource = visibleTables;
            this.Menu1.DataBind();
        }

    }
}
