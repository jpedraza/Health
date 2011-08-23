namespace Health.Core.Exceptions
{
    public class RepositoryException : BaseException
    {
        protected RepositoryException()
        {
        }

        protected RepositoryException(string message) : base(message)
        {
        }
    }
}