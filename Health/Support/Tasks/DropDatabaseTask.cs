using System.Data.Entity;

namespace Support.Tasks
{
    public class DropDatabaseTask : ITask
    {
        public void Process(DbContext context)
        {
            context.Database.Delete();
        }
    }
}
