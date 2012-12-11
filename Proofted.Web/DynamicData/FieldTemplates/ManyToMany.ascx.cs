namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.ComponentModel;
	using System.Data.Objects.DataClasses;
	using System.Web.UI;

	public partial class ManyToManyField : System.Web.DynamicData.FieldTemplateUserControl
    {
        protected override void OnDataBinding(EventArgs e)
        {
            base.OnDataBinding(e);

            object entity;
            ICustomTypeDescriptor rowDescriptor = this.Row as ICustomTypeDescriptor;
            if (rowDescriptor != null)
            {
                // Get the real entity from the wrapper
                entity = rowDescriptor.GetPropertyOwner(null);
            }
            else
            {
                entity = this.Row;
            }

            // Get the collection and make sure it's loaded
            RelatedEnd entityCollection = this.Column.EntityTypeProperty.GetValue(entity, null) as RelatedEnd;
            if (entityCollection == null)
            {
                throw new InvalidOperationException(String.Format("The ManyToMany template does not support the collection type of the '{0}' column on the '{1}' table.", this.Column.Name, this.Table.Name));
            }
            if (!entityCollection.IsLoaded)
            {
                entityCollection.Load();
            }

            // Bind the repeater to the list of children entities
            this.Repeater1.DataSource = entityCollection;
            this.Repeater1.DataBind();
        }

        public override Control DataControl
        {
            get
            {
                return this.Repeater1;
            }
        }

    }
}
