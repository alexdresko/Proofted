using System;
using System.Collections.Generic;

namespace Proofted.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class webpages_Membership
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        [MaxLength(128)]
        public string ConfirmationToken { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<System.DateTime> LastPasswordFailureDate { get; set; }
        public int PasswordFailuresSinceLastSuccess { get; set; }
        [MaxLength(128), Required]
        public string Password { get; set; }
        public Nullable<System.DateTime> PasswordChangedDate { get; set; }
        [MaxLength(128), Required]
        public string PasswordSalt { get; set; }
        [MaxLength(128)]
        public string PasswordVerificationToken { get; set; }
        public Nullable<System.DateTime> PasswordVerificationTokenExpirationDate { get; set; }
    }
}
