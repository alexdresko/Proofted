using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace Proofted.Web.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }


    }
}
