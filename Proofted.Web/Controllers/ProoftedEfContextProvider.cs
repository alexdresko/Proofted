namespace Proofted.Web.Controllers
{
	using System.Linq;
	using System.Web;

	using Breeze.WebApi;

	using Proofted.Web.Models.Proofing;
	using Proofted.Web.Models.Security;

	public class ProoftedEfContextProvider : EFContextProvider<ProoftedDbContext>
	{
		protected override bool BeforeSaveEntity(EntityInfo entityInfo)
		{
			var type = entityInfo.Entity.GetType();

			if (type == typeof(Organization))
			{
				var organization = entityInfo.Entity as Organization;

				if (entityInfo.EntityState == EntityState.Added)
				{
					var newOrganizationUser = new OrganizationUser() { UserId = HttpContext.Current.User.Identity.Name };

					organization.Users.Add(newOrganizationUser);

					return true;

				}

				if (entityInfo.EntityState == EntityState.Deleted)
				{
					var found = false;

					ProoftedDbContext.Borrow(
						context =>
							{ found = context.Organizations.Any(p => p.Users.Any(f => f.UserId == HttpContext.Current.User.Identity.Name)); });

					return found;
				}
				return true;

			}

			if (type == typeof(Invitation))
			{
				var invitation = entityInfo.Entity as Invitation;
				var found = false;


				if (entityInfo.EntityState == EntityState.Added)
				{

					ProoftedDbContext.Borrow(
						context =>
						{
							found =
								context.OrganizationUsers.Any(
									p => p.UserId == HttpContext.Current.User.Identity.Name && p.OrganizationId == invitation.OrganizationId);
						});

					if (found)
					{
						
					}

				}

				if (entityInfo.EntityState == EntityState.Deleted)
				{

					ProoftedDbContext.Borrow(
						context =>
						{
							found =
								context.OrganizationUsers.Any(
									p => p.UserId == HttpContext.Current.User.Identity.Name && p.Organization.Invitations.Any(a => a.InvitationId == invitation.InvitationId));
						});

				}
				return found;

			}

			if (type == typeof(Project))
			{
				var project = entityInfo.Entity as Project;
				var found = false;


				if (entityInfo.EntityState == EntityState.Added)
				{

					ProoftedDbContext.Borrow(
						context =>
						{
							found =
								context.OrganizationUsers.Any(
									p => p.UserId == HttpContext.Current.User.Identity.Name && p.OrganizationId == project.OrganizationId);
						});

				}

				if (entityInfo.EntityState == EntityState.Deleted)
				{

					ProoftedDbContext.Borrow(
						context =>
						{
							found =
								context.OrganizationUsers.Any(
									p => p.UserId == HttpContext.Current.User.Identity.Name && p.Organization.Projects.Any(a => a.ProjectId == project.ProjectId));
						});

				}
				return found;

			}

			return false;
		}
	}
}