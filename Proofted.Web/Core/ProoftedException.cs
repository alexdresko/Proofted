namespace Proofted.Web.Core
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