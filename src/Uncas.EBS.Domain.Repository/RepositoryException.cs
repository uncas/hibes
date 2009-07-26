using System;

namespace Uncas.EBS.Domain.Repository
{
    /// <summary>
    /// Represents exceptions that are thrown from the repository layer.
    /// </summary>
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RepositoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="innerException">The inner exception.</param>
        public RepositoryException(Exception innerException)
            : base("An exception has occurred in the repository."
            , innerException)
        {
        }
    }
}