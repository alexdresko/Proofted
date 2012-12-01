using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;
using Proofted.Web.Models.Mapping;

namespace Proofted.Web.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }

        public static T Borrow<T>(Func<UserDbContext, T> func)
        {
            using (var context = new UserDbContext())
            {
                return func(context);
            }
        }

        public static void DeleleAllObjects<TEntity>(DbSet<TEntity> objectSet,
                                              IEnumerable<TEntity> objects)
    where TEntity : class
        {

            foreach (var o in objects)
            {
                objectSet.Remove(o);
            }
        }

        public DbSet<webpages_Membership> webpages_Membership { get; set; }
        public DbSet<webpages_OAuthMembership> webpages_OAuthMembership { get; set; }
        public DbSet<webpages_Roles> webpages_Roles { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new webpages_MembershipMap());
            modelBuilder.Configurations.Add(new webpages_OAuthMembershipMap());
            modelBuilder.Configurations.Add(new webpages_RolesMap());
        }

        public static void Borrow(Action<UserDbContext> action)
        {
            using (var context = new UserDbContext())
            {
                action(context);
            }
        }

    }
}
