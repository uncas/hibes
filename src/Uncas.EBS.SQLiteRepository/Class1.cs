using System.Data.SQLite;

namespace Uncas.EBS.SQLiteRepository
{
    public class Class1
    {
        public void Setup()
        {
            SQLiteConnectionStringBuilder csBuilder
                = new SQLiteConnectionStringBuilder();
            csBuilder.DataSource = @"c:\temp\hibes.s3db";

            using (SQLiteConnection conn
                = new SQLiteConnection(csBuilder.ConnectionString))
            {
            }
        }
    }
}