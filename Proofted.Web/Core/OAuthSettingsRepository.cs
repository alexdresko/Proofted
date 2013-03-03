namespace Proofted.Web.Core
{
    using System.Collections.Generic;
    using System.Linq;

    using Proofted.Web.Models;
    using Proofted.Web.Models.Proofing;
    using Proofted.Web.Models.Security;

    public class OAuthSettingsRepository
    {
        #region Public Methods and Operators

        public static List<FaceBookAppCredential> GetFaceBookSettings()
        {
            return
                ProoftedDbContext.Borrow(
                    context => context.FaceBookAppCredentials.Where(credential => credential.Active).ToList());
        }

        #endregion
    }
}