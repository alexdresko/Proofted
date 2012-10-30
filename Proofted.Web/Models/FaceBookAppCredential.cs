using System;
using System.Collections.Generic;

namespace Proofted.Web.Models
{
    public class FaceBookAppCredential
    {
        public int FaceBookAppCredentialId { get; set; }
        public string AppId { get; set; }
        public string SecretKey { get; set; }
        public bool Active { get; set; }
    }
}
