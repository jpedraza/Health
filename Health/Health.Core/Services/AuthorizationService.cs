using System;
using System.Linq;
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
        /// <param name="actualDataAccessor">Репозиторий доступа к актуальным данным сессии.</param>
        /// <param name="permanentDataAccessor">Репозиторий доступа к сохраняемым данным сессии.</param>
        /// <param name="diKernel"></param>
        public AuthorizationService(IActualCredentialRepository actualDataAccessor,
                                    IPermanentCredentialRepository permanentDataAccessor, IDIKernel diKernel) : base(diKernel)
        {
            ActualDataAccessor = actualDataAccessor;
            PermanentDataAccessor = permanentDataAccessor;
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
                var defaultCredential = new UserCredential
                                             {
                                                 Login = DefaultRoles.Guest,
                                                 Role = DefaultRoles.Guest,
                                                 IsAuthorization = false,
                                                 IsRemember = false,
                                                 IsQuickUser = false
                                             };
                return defaultCredential;
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
                UserCredential userSessionInfo = ActualDataAccessor.Read(DefaultUserCredentialName);
                if (userSessionInfo == null)
                {
                    SessionStartup();
                    return ActualDataAccessor.Read(DefaultUserCredentialName);
                }
                return userSessionInfo;
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
        /// <param name="rememberMe">Запоминить?</param>
        /// <returns>Результат авторизации.</returns>
        public virtual bool Login(string login, string password, bool rememberMe = false)
        {
            Logger.Info(String.Format("Попытка авторизации пользователя: Логин - {0}, Пароль - {1}, Запоминать? - {2}.",
                                      login, password, rememberMe));

            User user = Get<IUserRepository>().GetByLoginAndPassword(login, password);

            if (user != null)
            {
                var credential = new UserCredential
                                     {
                                         Login = user.Login,
                                         Role = user.Role.Name,
                                         IsAuthorization = true,
                                         IsRemember = rememberMe,
                                         IsQuickUser = false
                                     };
                ActualDataAccessor.Write(DefaultUserCredentialName, credential);
                if (rememberMe)
                {
                    RememberMe();
                }
            }

            Logger.Info(String.Format("Результат авторизации пользователя {0} - {1} .", login,
                                      UserCredential.IsAuthorization));
            return user != null;
        }

        /// <summary>
        /// Быстрая авторизация пользователя по имени, фамилии, дате рождения и полюсу.
        /// </summary>
        /// <param name="firstName">Имя.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <param name="birthday">День рождения.</param>
        /// <param name="policy">Полюс.</param>
        /// <returns>Результат авторизации.</returns>
        public bool QuickLogin(string firstName, string lastName, DateTime birthday, string policy)
        {
            Patient patient = Get<IPatientRepository>().FindByFirstNameAndLastNameAndBirthdayAndPolicy(firstName,
                                                                                                       lastName,
                                                                                                       birthday, policy);
            if (patient != null)
            {
                ActualDataAccessor.Write(DefaultUserCredentialName, new UserCredential
                                                                      {
                                                                          IsAuthorization = true,
                                                                          IsRemember = false,
                                                                          Login = patient.Login,
                                                                          Role = DefaultRoles.QuickLogin,
                                                                          IsQuickUser = true
                                                                      });
                Logger.Info(String.Format("Пользователь {0} {1} совершил быстрый вход в систему.", patient.Id, patient.FullName));
                return true;
            }
            return false;
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
            User user = Get<IUserRepository>().GetByLogin(credential.Login);
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