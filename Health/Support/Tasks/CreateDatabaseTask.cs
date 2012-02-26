using System.Data.Entity;

namespace Support.Tasks
{
    public class CreateDatabaseTask : ITask
    {
        #region Implementation of ITask

        public void Process(DbContext context)
        {
            context.Database.CreateIfNotExists();
        }

        #endregion
    }
}