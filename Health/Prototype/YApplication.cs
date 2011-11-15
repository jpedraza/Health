using System;
using System.Collections.Generic;
using Prototype.Forms;
using System.Windows.Forms;
using Prototype.HealthDatabaseDataSetTableAdapters;

namespace Prototype
{
    internal class YApplication : ApplicationContext
    {
        private readonly AuthorizationForm _authorizationForm;

        internal YApplication()
        {
            _authorizationForm = new AuthorizationForm
                                        {
                                            OnAuthorize = OnAuthorize,
                                            OnClose = Close
                                        };
            _authorizationForm.Show();
        }

        internal void Close()
        {
            if (!YIoc.Get<YAuthorization>().IsAuthorize)
            {
                Application.Exit();
            }
        }

        internal string OnAuthorize(string login, string password)
        {
            try
            {
                YIoc.Get<YAuthorization>().Authorize(login, password);
            }
            catch (Exception)
            {
                return "sorry, database error.";
            }
            if (YIoc.Get<YAuthorization>().IsAuthorize)
            {
                _authorizationForm.Close();
                MainForm = new MainForm();
                MainForm.Show();
                return string.Empty;
            }
            return "authorization failed.";
        }
    }

    internal class YAuthorization
    {
        public bool IsAuthorize { get; private set; }

        internal void Authorize(string login, string password)
        {
            int count = Convert.ToInt32(YIoc.Get<UsersTableAdapter>().AuthorizeByLoginAndPassword(login, password));
            IsAuthorize = count == 1;
        }
    }

    internal static class YIoc
    {
        private static readonly IDictionary<Type, object> _cache;

        static YIoc()
        {
            _cache = new Dictionary<Type, object>();
        }

        internal static T Get<T>() where T : class, new()
        {
            Type tt = typeof (T);
            if (_cache.ContainsKey(tt))
            {
                return _cache[tt] as T;
            }
            var obj = new T();
            _cache.Add(tt, obj);
            return obj;
        }
    }
}
