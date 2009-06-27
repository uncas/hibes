using System;

namespace Uncas.EBS.Domain.Repository
{
    public class RepositoryException : Exception
    {
        public RepositoryException()
        {
        }

        public RepositoryException(string message)
            : base(message)
        {
        }

        public RepositoryException(Exception innerException)
            : base("An exception has occurred in the repository."
            , innerException)
        {
        }
 
        public RepositoryException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}