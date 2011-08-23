using System.Collections.Generic;
using Health.Core.API.Repository;
using Health.Core.Entities;

namespace Health.Data.Repository.Fake
{
    public class FakePermanentCredentialRepository : IPermanentCredentialRepository
    {
        private readonly Dictionary<string, object> _storage;

        public FakePermanentCredentialRepository()
        {
            _storage = new Dictionary<string, object>();
        }

        #region IPermanentCredentialRepository Members

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
            return _storage.ContainsKey(identifier) ? (UserCredential) _storage[identifier] : null;
        }

        public void Clear()
        {
            _storage.Clear();
        }

        #endregion
    }
}