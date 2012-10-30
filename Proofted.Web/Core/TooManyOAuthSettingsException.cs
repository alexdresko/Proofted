namespace Proofted.Web.Core
{
    public class TooManyOAuthSettingsException : ProoftedException
    {
        public TooManyOAuthSettingsException(string settingType) : base(string.Format("For type {0}", settingType ?? "unknown"))
        {
            
        }
    }
}