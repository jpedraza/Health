using System;
using Health.Core.API;
using Health.Core.API.Repository;
using Health.Core.API.Services;
using Health.Core.Entities;
using Health.Core.Entities.POCO;

namespace Health.Core.Services
{
    /// <summary>
    /// Сервис авторизации.
    /// </summary>
    public class AuthorizationService : CoreService, IAuthorizationService
    {
        /// <summary>
        /// Инициализация репозиториев доступа к анным сессии.
        /// </summary>
        /// <param name="actual_data_accessor">Репозиторий доступа к актуальным данным сессии.</param>
        /// <param name="permanent_data_accessor">Репозиторий доступа к сохраняемым данным сессии.</param>
        /// <param name="di_kernel"></param>
        /// <param name="core_kernel"></param>
        public AuthorizationService(IActualCredentialRepository actual_data_accessor,
                                    IPermanentCredentialRepository permanent_data_accessor, IDIKernel di_kernel,
                                    ICoreKernel core_kernel) : base(di_kernel, core_kernel)
        {
            ActualDataAccessor = actual_data_accessor;
            PermanentDataAccessor = permanent_data_accessor;
        }

        #region IAuthorizationService Members

        /// <summary>
        /// Доступ к актуальному хранлищу сессии пользователя.
        /// </summary>
        public IActualCredentialRepository ActualDataAccessor { get; set; }

        /// <summary>
        /// Доступ к постоянному хранлищу сессии пользователя.
        /// </summary>
        public IPermanentCredentialRepository PermanentDataAccessor { get; set; }

        /// <summary>
        /// Дефолтные роли пользователя.
        /// </summary>
        public DefaultRoles DefaultRoles
        {
            get { return new DefaultRoles(); }
            set { }
        }

        /// <summary>
        /// Дефолтное имя переменной в сессии куда сохраняется мандат пользователя.
        /// </summary>
        public string DefaultUserCredentialName
        {
            get { return "remember"; }
            set { }
        }

        /// <summary>
        /// Дефолтный мандат пользователя.
        /// </summary>
        public UserCredential DefaultUserCredential
        {
            get
            {
                var default_credential = new UserCredential
                                             {
                                                 Login = DefaultRoles.Guest.Name,
                                                 Role = DefaultRoles.Guest.Name,
                                                 IsAuthirization = false,
                                                 IsRemember = false
                                             };
                return default_credential;
            }
            set { }
        }

        /// <summary>
        /// Актуальный мандат пользователя.
        /// </summary>
        public virtual UserCredential UserCredential
        {
            get
            {
                UserCredential user_session_info = ActualDataAccessor.Read(DefaultUserCredentialName);
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
        /// Стартовать сессию.
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
            Logger.Info(String.Format("Сессия для пользователя {0} запущена.", UserCredential.Login));
        }

        /// <summary>
        /// Вход пользователя.
        /// </summary>
        /// <param name="login">Логин.</param>
        /// <param name="password">Пароль.</param>
        /// <param name="remember_me">Запоминить?</param>
        /// <returns>Результат авторизации.</returns>
        public virtual bool Login(string login, string password, bool remember_me = false)
        {
            Logger.Info(String.Format("Попытка авторизации пользователя: Логин - {0}, Пароль - {1}, Запоминать? - {2}.",
                                      login, password, remember_me));

            User user = CoreKernel.UserRepo.GetByLoginAndPassword(login, password);

            if (user != null)
            {
                ActualDataAccessor.Write(DefaultUserCredentialName, new UserCredential
                                                                        {
                                                                            Login =
                                                                                user.Login,
                                                                            Role =
                                                                                user.Role.Name,
                                                                            IsAuthirization
                                                                                = true,
                                                                            IsRemember =
                                                                                remember_me
                                                                        });
                if (remember_me)
                {
                    RememberMe();
                }
            }

            Logger.Info(String.Format("Результат авторизации пользователя {0} - {1} .", login,
                                      UserCredential.IsAuthirization));
            return user != null;
        }

        /// <summary>
        /// Сброс сессии для пользователя.
        /// </summary>
        public virtual void Logout()
        {
            Logger.Info(String.Format("Сессия пользователя {0} сброшена.", UserCredential.Login));
            ActualDataAccessor.Clear();
            PermanentDataAccessor.Clear();
            ActualDataAccessor.Write(DefaultUserCredentialName, DefaultUserCredential);
        }

        /// <summary>
        /// Запоминаем пользователя.
        /// </summary>
        public virtual void RememberMe()
        {
            Logger.Info(String.Format("Пользователь {0} был запомнен.", UserCredential.Login));
            PermanentDataAccessor.Write("remember", UserCredential);
        }

        /// <summary>
        /// Запомнен ли пользователь.
        /// </summary>
        /// <returns>Да или нет.</returns>
        public virtual bool IsRemember()
        {
            UserCredential credential = PermanentDataAccessor.Read("remember");
            if (credential == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Восстановление запомненной сессии.
        /// </summary>
        public virtual void RestoreRememberSession()
        {
            UserCredential credential = PermanentDataAccessor.Read("remember");
            User user = CoreKernel.UserRepo.GetByLogin(credential.Login);
            if (user != null)
            {
                ActualDataAccessor.Write(DefaultUserCredentialName, credential);
            }
            Logger.Info(String.Format("Для пользователя {0} была восстановлена запомненная сессия.",
                                      UserCredential.Login));
        }

        #endregion
    }
}