using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proofted.Web.Models
{
    using System.Data.Entity;

    public class ProoftedDbContext : DbContext
    {
        public ProoftedDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<FaceBookAppCredential> FaceBookAppCredentials { get; set; }
    }
}