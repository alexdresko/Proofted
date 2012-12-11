namespace Proofted.Web.DynamicData.EntityTemplates
{
	using System;
	using System.Web.DynamicData;
	using System.Web.UI;
	using System.Web.UI.WebControls;

	public partial class DefaultEntityTemplate : System.Web.DynamicData.EntityTemplateUserControl
    {
        private MetaColumn currentColumn;

        protected override void OnLoad(EventArgs e)
        {
            foreach (MetaColumn column in this.Table.GetScaffoldColumns(this.Mode, this.ContainerType))
            {
                this.currentColumn = column;
                Control item = new _NamingContainer();
                this.EntityTemplate1.ItemTemplate.InstantiateIn(item);
                this.EntityTemplate1.Controls.Add(item);
            }
        }

        protected void Label_Init(object sender, EventArgs e)
        {
            Label label = (Label)sender;
            label.Text = this.currentColumn.DisplayName;
        }

        protected void DynamicControl_Init(object sender, EventArgs e)
        {
            DynamicControl dynamicControl = (DynamicControl)sender;
            dynamicControl.DataField = this.currentColumn.Name;
        }

        public class _NamingContainer : Control, INamingContainer { }

    }
}
