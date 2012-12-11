namespace Proofted.Web.DynamicData.Content
{
	using System;
	using System.Globalization;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class GridViewPager : System.Web.UI.UserControl
    {
        private GridView _gridView;

        protected void Page_Load(object sender, EventArgs e)
        {
            Control c = this.Parent;
            while (c != null)
            {
                if (c is GridView)
                {
                    this._gridView = (GridView)c;
                    break;
                }
                c = c.Parent;
            }
        }

        protected void TextBoxPage_TextChanged(object sender, EventArgs e)
        {
            if (this._gridView == null)
            {
                return;
            }
            int page;
            if (int.TryParse(this.TextBoxPage.Text.Trim(), out page))
            {
                if (page <= 0)
                {
                    page = 1;
                }
                if (page > this._gridView.PageCount)
                {
                    page = this._gridView.PageCount;
                }
                this._gridView.PageIndex = page - 1;
            }
            this.TextBoxPage.Text = (this._gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
        }

        protected void DropDownListPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._gridView == null)
            {
                return;
            }
            DropDownList dropdownlistpagersize = (DropDownList)sender;
            this._gridView.PageSize = Convert.ToInt32(dropdownlistpagersize.SelectedValue, CultureInfo.CurrentCulture);
            int pageindex = this._gridView.PageIndex;
            this._gridView.DataBind();
            if (this._gridView.PageIndex != pageindex)
            {
                //if page index changed it means the previous page was not valid and was adjusted. Rebind to fill control with adjusted page
                this._gridView.DataBind();
            }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (this._gridView != null)
            {
                this.LabelNumberOfPages.Text = this._gridView.PageCount.ToString(CultureInfo.CurrentCulture);
                this.TextBoxPage.Text = (this._gridView.PageIndex + 1).ToString(CultureInfo.CurrentCulture);
                this.DropDownListPageSize.SelectedValue = this._gridView.PageSize.ToString(CultureInfo.CurrentCulture);
            }
        }

    }
}
