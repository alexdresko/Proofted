namespace Proofted.Web.Models.Security
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class UserProfile
    {
        public UserProfile()
        {
            this.webpages_Roles = new List<webpages_Roles>();
        }

        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(56)]
        public string UserName { get; set; }
        public virtual ICollection<webpages_Roles> webpages_Roles { get; set; }
    }
}
