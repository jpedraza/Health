using System.Data.Entity;

namespace Support
{
    internal interface ITask
    {
        void Process(DbContext context);
    }
}