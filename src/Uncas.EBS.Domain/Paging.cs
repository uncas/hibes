using System;

namespace Uncas.EBS.Domain
{
    /// <summary>
    /// Handles paging.
    /// </summary>
    public class Paging
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        public Paging()
            : this(1, 100)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Paging"/> class.
        /// </summary>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">Size of the page.</param>
        public Paging(
            int pageNumber,
            int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(
                    "pageNumber", 
                    "Page number must be positive.");
            }

            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        /// <value>
        /// The page number.
        /// </value>
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        public int PageSize { get; set; }

        /// <summary>
        /// Gets the number to skip.
        /// </summary>
        /// <value>The number of items to skip.</value>
        public int Skip
        {
            get
            {
                return ((PageNumber - 1) * PageSize) + 1;
            }
        }
    }
}
