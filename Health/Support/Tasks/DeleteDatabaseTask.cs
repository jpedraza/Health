using System.Data.Entity;

namespace Support.Tasks
{
    public class DeleteDatabaseTask : ITask
    {
        #region Implementation of ITask

        public void Process(DbContext context)
        {
            context.Database.Delete();
        }

        #endregion
    }
}