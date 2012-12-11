namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Web.UI;

	public partial class ChildrenField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private bool _allowNavigation = true;
        private string _navigateUrl;

        public string NavigateUrl
        {
            get
            {
                return this._navigateUrl;
            }
            set
            {
                this._navigateUrl = value;
            }
        }

        public bool AllowNavigation
        {
            get
            {
                return this._allowNavigation;
            }
            set
            {
                this._allowNavigation = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.HyperLink1.Text = "View " + this.ChildrenColumn.ChildTable.DisplayName;
        }

        protected string GetChildrenPath()
        {
            if (!this.AllowNavigation)
            {
                return null;
            }

            if (String.IsNullOrEmpty(this.NavigateUrl))
            {
                return this.ChildrenPath;
            }
            else
            {
                return this.BuildChildrenPath(this.NavigateUrl);
            }
        }

        public override Control DataControl
        {
            get
            {
                return this.HyperLink1;
            }
        }

    }
}
