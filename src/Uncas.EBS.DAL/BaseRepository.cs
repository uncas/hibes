using System;

namespace Uncas.EBS.DAL
{
    public abstract class BaseRepository : IDisposable
    {
        protected BaseRepository()
        {
            _db = new EBSDataContext();
            _disposed = false;
        }

        private bool _disposed;

        private EBSDataContext _db;
        protected EBSDataContext DB
        {
            get
            {
                return _db;
            }
        }

        protected void SubmitChanges()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException("Resource was disposed.");
            }
            DB.SubmitChanges();
        }

        protected static double? GetDoubleFromDecimal
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

        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }

        #endregion

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