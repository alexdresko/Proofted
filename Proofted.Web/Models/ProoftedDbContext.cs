using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Proofted.Web.Models
{
    using System.Data.Entity;

    using Proofted.Web.Models.Mapping;
    using Proofted.Web.Models.Proofing;
    using Proofted.Web.Models.Security;

    public class ProoftedDbContext : DbContext
    {
        public ProoftedDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<FaceBookAppCredential> FaceBookAppCredentials { get; set; }

		//public DbSet<Project> Projects { get; set; }

 

        public static T Borrow<T>(Func<ProoftedDbContext, T> func)
        {
            using (var context = new ProoftedDbContext())
            {
                return func(context);
            }
        }

        public static void Borrow(Action<ProoftedDbContext> action)
        {
            using (var context = new ProoftedDbContext())
            {
                action(context);
            }
        }

    }
}