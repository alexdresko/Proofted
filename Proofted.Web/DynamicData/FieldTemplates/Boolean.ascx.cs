﻿namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Web.UI;

	public partial class BooleanField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object val = this.FieldValue;
            if (val != null)
                this.CheckBox1.Checked = (bool)val;
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
