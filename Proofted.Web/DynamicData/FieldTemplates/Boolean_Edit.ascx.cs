namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Collections.Specialized;
	using System.Web.UI;

	public partial class Boolean_EditField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object val = this.FieldValue;
            if (val != null)
                this.CheckBox1.Checked = (bool)val;
        }

        protected override void ExtractValues(IOrderedDictionary dictionary)
        {
            dictionary[this.Column.Name] = this.CheckBox1.Checked;
        }

        public override Control DataControl
        {
            get
            {
                return this.CheckBox1;
            }
        }

    }
}
