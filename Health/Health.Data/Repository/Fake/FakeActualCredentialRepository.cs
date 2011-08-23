using System.Collections.Generic;
using Health.Core.API.Repository;
using Health.Core.Entities;

namespace Health.Data.Repository.Fake
{
    public class FakeActualCredentialRepository : IActualCredentialRepository
    {
        private readonly Dictionary<string, object> _storage;

        public FakeActualCredentialRepository()
        {
            _storage = new Dictionary<string, object>();
        }

        #region IActualCredentialRepository Members

        public void Write(string identifier, UserCredential credential)
        {
            if (!_storage.ContainsKey(identifier))
            {
                _storage.Add(identifier, credential);
            }
            else
            {
                _storage[identifier] = credential;
            }
        }

        public UserCredential Read(string identifier)
        {
            return (UserCredential) _storage[identifier];
        }

        public void Clear()
        {
            _storage.Clear();
        }

        #endregion
    }
}