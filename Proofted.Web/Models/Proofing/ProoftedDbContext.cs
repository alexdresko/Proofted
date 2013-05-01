namespace Proofted.Web.Models.Proofing
{
	using System;
	using System.Data.Entity;

	using Proofted.Web.Models.Security;

	public class ProoftedDbContext : DbContext
    {
        public ProoftedDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<FaceBookAppCredential> FaceBookAppCredentials { get; set; }

		public DbSet<Organization> Organizations { get; set; }

		//public DbSet<OrganizationGroup> OrganizationGroups { get; set; }

	    public DbSet<Project> Projects { get; set; }

	    public DbSet<Tag> Tags { get; set; }

		public DbSet<Invitation> Invitations { get; set; }

	    public DbSet<ProjectFile> ProjectFiles { get; set; }

	    public DbSet<CommentThread> CommentThreads { get; set; }

		public DbSet<OrganizationUser> OrganizationUsers { get; set; }

	    public DbSet<Comment> Comments { get; set; }

	    public DbSet<Approver> Approvers { get; set; }

		public DbSet<Log> Logs { get; set; }

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