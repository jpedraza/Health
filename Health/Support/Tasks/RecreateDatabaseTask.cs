using System.Data.Entity;

namespace Support.Tasks
{
    public class RecreateDatabaseTask : ITask
    {
        #region Implementation of ITask

        public void Process(DbContext context)
        {
            context.Database.Delete();
            context.Database.Create();
        }

        #endregion
    }
}