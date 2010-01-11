using System;

namespace Uncas.EBS.DAL
{
    /// <summary>
    /// Base repository for repositories.
    /// </summary>
    public abstract class BaseRepository : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository"/> class.
        /// </summary>
        protected BaseRepository()
        {
            db = new EBSDataContext();
        }

        private bool disposed;

        private EBSDataContext db;

        /// <summary>
        /// Gets the DB.
        /// </summary>
        /// <value>The data context.</value>
        protected internal EBSDataContext DB
        {
            get
            {
                return db;
            }
        }

        /// <summary>
        /// Gets the double from decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>The double.</returns>
        protected internal static double? GetDoubleFromDecimal
            (decimal? value)
        {
            double? result = null;
            if (value.HasValue)
            {
                result = (double)value.Value;
            }

            return result;
        }

        /// <summary>
        /// Submits the changes.
        /// </summary>
        protected internal void SubmitChanges()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("Resource was disposed.");
            }

            DB.SubmitChanges();
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        #endregion

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>True</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!disposed)
            {
                if (disposing)
                {
                    if (db != null)
                    {
                        db.Dispose();
                    }
                }

                // Indicate that the instance has been disposed.
                db = null;
                disposed = true;
            }
        }
    }
}