using System;
using System.Collections.Generic;
using Health.API.Entities;
using Health.API.Repository;

namespace Health.Data.Repository.Fake
{
    public class FakePermanentCredentialRepository : IPermanentCredentialRepository
    {
        private readonly Dictionary<string, object> _storage;

        public FakePermanentCredentialRepository()
        {
            _storage = new Dictionary<string, object>();
        }

        public void Write(string identifier, IUserCredential credential)
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

        public IUserCredential Read(string identifier)
        {
            return _storage.ContainsKey(identifier) ? (IUserCredential) _storage[identifier] : null;
        }

        public void Clear()
        {
            _storage.Clear();
        }
    }
}