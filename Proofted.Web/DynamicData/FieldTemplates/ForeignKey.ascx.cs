namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Web.UI;

	public partial class ForeignKeyField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private bool _allowNavigation = true;

        public string NavigateUrl
        {
            get;
            set;
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

        protected string GetDisplayString()
        {
            object value = this.FieldValue;

            if (value == null)
            {
                return this.FormatFieldValue(this.ForeignKeyColumn.GetForeignKeyString(this.Row));
            }
            else
            {
                return this.FormatFieldValue(this.ForeignKeyColumn.ParentTable.GetDisplayString(value));
            }
        }

        protected string GetNavigateUrl()
        {
            if (!this.AllowNavigation)
            {
                return null;
            }

            if (String.IsNullOrEmpty(this.NavigateUrl))
            {
                return this.ForeignKeyPath;
            }
            else
            {
                return this.BuildForeignKeyPath(this.NavigateUrl);
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
