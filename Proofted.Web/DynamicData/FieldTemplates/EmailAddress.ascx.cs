namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Web.UI;

	public partial class EmailAddressField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            string url = this.FieldValueString;
            if (!url.StartsWith("mailto:", StringComparison.OrdinalIgnoreCase))
            {
                url = "mailto:" + url;
            }
            this.HyperLink1.NavigateUrl = url;
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
