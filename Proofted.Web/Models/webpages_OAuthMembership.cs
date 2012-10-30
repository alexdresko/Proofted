using System;
using System.Collections.Generic;

namespace Proofted.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class webpages_OAuthMembership
    {
        [Required, MaxLength(30)]
        public string Provider { get; set; }
        [Required, MaxLength(100)]
        public string ProviderUserId { get; set; }
        [Key]
        public int UserId { get; set; }
    }
}
