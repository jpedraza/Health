using System;
using Health.API.Entities;
using Health.API.Repository;
using Health.API.Services;
using Ninject;

namespace Health.Core.Services
{
    public class AuthorizationService<TUserCredential> : CoreService, IAuthorizationService<IUserCredential>
        where TUserCredential : IUserCredential, new()
    {
        /// <summary>
        /// Инициализация репозиториев доступа к анным сессии
        /// </summary>
        /// <param name="actual_data_accessor"></param>
        /// <param name="permanent_data_accessor"></param>
        public AuthorizationService(IActualCredentialRepository actual_data_accessor,
                                    IPermanentCredentialRepository permanent_data_accessor)
        {
            ActualDataAccessor = actual_data_accessor;
            PermanentDataAccessor = permanent_data_accessor;
        }

        /// <summary>
        /// Доступ к актуальному хранлищу сессии пользователя
        /// </summary>
        public IActualCredentialRepository ActualDataAccessor { get; set; }

        /// <summary>
        /// Доступ к постоянному хранлищу сессии пользователя
        /// </summary>
        public IPermanentCredentialRepository PermanentDataAccessor { get; set; }

        /// <summary>
        /// Дефолтные роли пользователя
        /// </summary>
        public IDefaultRoles DefaultRoles
        {
            get { return Instance<IDefaultRoles>(); }
            set {}
        }

        /// <summary>
        /// Дефолтное имя переменной в сессии куда сохраняется мандат пользователя
        /// </summary>
        public string DefaultUserCredentialName { get { return "remember"; } set { } }

        /// <summary>
        /// Дефолтный мандат пользователя
        /// </summary>
        public IUserCredential DefaultUserCredential
        {
            get
            {
                var default_credential = Instance<IUserCredential>();
                default_credential.Login = DefaultRoles.Guest.Name;
                default_credential.Role = DefaultRoles.Guest.Name;
                default_credential.IsAuthirization = false;
                default_credential.IsRemember = false;
                return default_credential;
            }
            set {}
        }

        /// <summary>
        /// Актуальный мандат пользователя
        /// </summary>
        public virtual IUserCredential UserCredential
        {
            get
            {
                IUserCredential user_session_info = ActualDataAccessor.Read(DefaultUserCredentialName);
                if (user_session_info == null)
                {
                    SessionStartup();
                    return ActualDataAccessor.Read(DefaultUserCredentialName);
                }
                return user_session_info;
            }
            set { ActualDataAccessor.Write(DefaultUserCredentialName, value); }
        }

        /// <summary>
        /// Стартовать сессию
        /// </summary>
        public virtual void SessionStartup()
        {
            if (!IsRemember())
            {
                UserCredential = DefaultUserCredential;
            }
            else
            {
                RestoreRememberSession();
            }
        }

        /// <summary>
        /// Вход пользователя
        /// </summary>
        /// <param name="login">Логин</param>
        /// <param name="password">Пароль</param>
        /// <param name="remember_me">Запоминить?</param>
        /// <returns>Результат авторизации</returns>
        public virtual bool Login(string login, string password, bool remember_me = false)
        {
            IUser user = CoreKernel.UserRepo.GetByLoginAndPassword(login, password);

            if (user != null)
            {
                ActualDataAccessor.Write(DefaultUserCredentialName, new TUserCredential
                                                                        {
                                                                            Login = user.Login,
                                                                            Role = user.Role.Name,
                                                                            IsAuthirization = true,
                                                                            IsRemember = remember_me
                                                                        });
                if (remember_me)
                {
                    RememberMe();
                }
            }

            return user != null;
        }

        /// <summary>
        /// Сброс сессии для пользователя
        /// </summary>
        public virtual void Logout()
        {
            ActualDataAccessor.Clear();
            PermanentDataAccessor.Clear();
            ActualDataAccessor.Write(DefaultUserCredentialName, DefaultUserCredential);
        }

        /// <summary>
        /// Запоминаем пользователя
        /// </summary>
        public virtual void RememberMe()
        {
            PermanentDataAccessor.Write("remember", UserCredential);
        }

        /// <summary>
        /// Запомнен ли пользователь
        /// </summary>
        /// <returns>Да или нет :)</returns>
        public virtual bool IsRemember()
        {
            IUserCredential credential = PermanentDataAccessor.Read("remember");
            if (credential == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Восстановление запомненной сессии
        /// </summary>
        public virtual void RestoreRememberSession()
        {
            IUserCredential credential = PermanentDataAccessor.Read("remember");
            IUser user = CoreKernel.UserRepo.GetByLogin(credential.Login);
            if (user != null)
            {
                ActualDataAccessor.Write(DefaultUserCredentialName, credential);
            }
        }
    }
}