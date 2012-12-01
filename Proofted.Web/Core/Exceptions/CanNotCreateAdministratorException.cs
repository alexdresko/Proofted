namespace Proofted.Web.Core.Exceptions
{
    public class CanNotCreateAdministratorException : ProoftedException
    {
        public CanNotCreateAdministratorException()
            : base("An Administrator role has been created, but you must manually associate a user")
        {
            
        }
    }
}