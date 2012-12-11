namespace Proofted.Web.Models.Security
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class webpages_Roles
    {
        public webpages_Roles()
        {
            this.UserProfiles = new List<UserProfile>();
        }

        [Key]
        public int RoleId { get; set; }
        [Required, MaxLength(56)]
        public string RoleName { get; set; }
        public virtual ICollection<UserProfile> UserProfiles { get; set; }
    }
}
