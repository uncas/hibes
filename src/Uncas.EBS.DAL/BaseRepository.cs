namespace Uncas.EBS.DAL
{
    public abstract class BaseRepository
    {
        protected EBSDataContext db = new EBSDataContext();

        protected void SubmitChanges()
        {
            db.SubmitChanges();
        }

        protected double? GetDoubleFromDecimal
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
    }
}