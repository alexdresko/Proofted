namespace Proofted.Web.DynamicData.FieldTemplates
{
	using System;
	using System.Web.DynamicData;
	using System.Web.UI;

	public partial class EnumerationField : System.Web.DynamicData.FieldTemplateUserControl
    {
        public override Control DataControl
        {
            get
            {
                return this.Literal1;
            }
        }

        public string EnumFieldValueString
        {
            get
            {
                if (this.FieldValue == null)
                {
                    return this.FieldValueString;
                }

                Type enumType = this.Column.GetEnumType();
                if (enumType != null)
                {
                    object enumValue = System.Enum.ToObject(enumType, this.FieldValue);
                    return this.FormatFieldValue(enumValue);
                }

                return this.FieldValueString;
            }
        }

    }
}
