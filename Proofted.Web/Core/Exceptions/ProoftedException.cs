namespace Proofted.Web.Core.Exceptions
{
    using System;

    public class ProoftedException : Exception
    {
        public ProoftedException()
        {
            
        }

        public ProoftedException(string message) : base(message)
        {
        }
    }
}