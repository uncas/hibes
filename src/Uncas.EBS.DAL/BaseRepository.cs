namespace Uncas.EBS.DAL
{
    public abstract class BaseRepository
    {
        protected EBSDataContext db = new EBSDataContext();

        protected void SubmitChanges()
        {
            db.SubmitChanges();
        }
    }
}