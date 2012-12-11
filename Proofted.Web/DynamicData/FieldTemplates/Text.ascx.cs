namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System.Web.DynamicData;
	using System.Web.UI;

	public partial class TextField : System.Web.DynamicData.FieldTemplateUserControl
    {
        private const int MAX_DISPLAYLENGTH_IN_LIST = 25;

        public override string FieldValueString
        {
            get
            {
                string value = base.FieldValueString;
                if (this.ContainerType == ContainerType.List)
                {
                    if (value != null && value.Length > MAX_DISPLAYLENGTH_IN_LIST)
                    {
                        value = value.Substring(0, MAX_DISPLAYLENGTH_IN_LIST - 3) + "...";
                    }
                }
                return value;
            }
        }

        public override Control DataControl
        {
            get
            {
                return this.Literal1;
            }
        }

    }
}
