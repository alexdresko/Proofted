namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Web.UI;

	public partial class UrlField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            this.HyperLinkUrl.NavigateUrl = this.ProcessUrl(this.FieldValueString);
        }

        private string ProcessUrl(string url)
        {
            if (url.StartsWith("http://", StringComparison.OrdinalIgnoreCase) || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            {
                return url;
            }

            return "http://" + url;
        }

        public override Control DataControl
        {
            get
            {
                return this.HyperLinkUrl;
            }
        }

    }
}
