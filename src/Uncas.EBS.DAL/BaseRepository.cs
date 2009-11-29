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
            _db = new EBSDataContext();
        }

        private bool _disposed;

        private EBSDataContext _db;
        /// <summary>
        /// Gets the DB.
        /// </summary>
        /// <value>The DB.</value>
        internal protected EBSDataContext DB
        {
            get
            {
                return _db;
            }
        }

        /// <summary>
        /// Submits the changes.
        /// </summary>
        internal protected void SubmitChanges()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Resource was disposed.");
            }
            DB.SubmitChanges();
        }

        /// <summary>
        /// Gets the double from decimal.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        internal protected static double? GetDoubleFromDecimal
            (
            decimal? value
            )
        {
            double? result = null;
            if (value.HasValue)
            {
                result = (double)value.Value;
            }
            return result;
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
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            // If you need thread safety, use a lock around these 
            // operations, as well as in your methods that use the resource.
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_db != null)
                    {
                        _db.Dispose();
                    }
                }

                // Indicate that the instance has been disposed.
                _db = null;
                _disposed = true;
            }
        }
    }
}