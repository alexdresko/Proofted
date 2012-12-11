namespace Proofted.Web.Models.Security
{
    public class FaceBookAppCredential
    {
        public int FaceBookAppCredentialId { get; set; }
        public string AppId { get; set; }
        public string SecretKey { get; set; }
        public bool Active { get; set; }
    }
}
