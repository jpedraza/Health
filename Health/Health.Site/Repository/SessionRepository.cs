using System.Collections.Specialized;
using System.Web;
using System.Web.SessionState;
using Health.Core.API.Repository;
using Health.Core.Entities;

namespace Health.Site.Repository
{
    public class SessionRepository : IActualCredentialRepository
    {
        protected HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        #region IActualCredentialRepository Members

        public void Write(string identifier, UserCredential credential)
        {
            if (!SessionContainsKey(identifier))
            {
                Session.Add(identifier, credential);
            }
            else
            {
                Session[identifier] = credential;
            }
        }

        public UserCredential Read(string identifier)
        {
            return (UserCredential) Session[identifier];
        }

        public void Clear()
        {
            Session.Clear();
        }

        #endregion

        protected bool SessionContainsKey(string key)
        {
            NameObjectCollectionBase.KeysCollection keys_collection = Session.Keys;

            foreach (object keyi in keys_collection)
            {
                if (keyi.ToString() == key)
                {
                    return true;
                }
            }
            return false;
        }
    }
}