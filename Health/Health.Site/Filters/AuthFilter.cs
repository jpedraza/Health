using System;
using System.Web.Mvc;
using System.Web.Routing;
using Health.Core.API;
using Health.Core.API.Services;
using Health.Core.Entities;

namespace Health.Site.Filters
{
    public class AuthFilter : IAuthorizationFilter
    {
        /// <summary>
        /// Куда переадресуем если запрещен доступ
        /// </summary>
        protected readonly RedirectToRouteResult RedirectResult = new RedirectToRouteResult(
            new RouteValueDictionary(new {area = "Account", controller = "Authorization", action = "Login"}));

        protected readonly RedirectToRouteResult RedirectResultForQuickLogin = new RedirectToRouteResult(
            new RouteValueDictionary(new {area = "", controller = "Appointment", action = "Index"}));

        /// <summary>
        /// Разрешенные роли (Приоритет выше, чем у DenyRoles)
        /// </summary>
        public string AllowRoles;

        /// <summary>
        /// Запрещенные роли
        /// </summary>
        public string DenyRoles;

        private string _userRole;

        public AuthFilter(IDIKernel diKernel, string allowRoles, string denyRoles)
        {
            DIKernel = diKernel;
            AllowRoles = allowRoles;
            DenyRoles = denyRoles;
        }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        private string UserRole
        {
            get
            {
                if (String.IsNullOrEmpty(_userRole))
                {
                    _userRole = DIKernel.Get<IAuthorizationService>().UserCredential.Role;
                }
                return _userRole;
            }
        }

        /// <summary>
        /// Центральный сервис
        /// </summary>
        protected IDIKernel DIKernel { get; set; }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filterContext)
        {
            // Если у пользователя вообще нет роли (никакой ?)
            if (String.IsNullOrEmpty(DIKernel.Get<IAuthorizationService>().UserCredential.Role))
            {
                // Сбрасываем его сессию до гостя
                DIKernel.Get<IAuthorizationService>().Logout();
            }

            // Если не заданы права доступа
            if (String.IsNullOrEmpty(AllowRoles) & String.IsNullOrEmpty(DenyRoles))
            {
                // Считаем что всем авторизованным пользователям разрешен доступ
                if (!DIKernel.Get<IAuthorizationService>().UserCredential.IsAuthorization)
                {
                    filterContext.Result = RedirectResult;
                    return;
                }
            }

            // Если указаны только права на запрет доступа
            if (String.IsNullOrEmpty(AllowRoles) & !String.IsNullOrEmpty(DenyRoles))
            {
                filterContext.Result = OnlyDenyPermission(filterContext.Result);
                return;
            }

            // Если указаны права только на разрешение доступа
            if (!String.IsNullOrEmpty(AllowRoles) & String.IsNullOrEmpty(DenyRoles))
            {
                filterContext.Result = OnlyAllowPermission(filterContext.Result);
                return;
            }


            if (!String.IsNullOrEmpty(AllowRoles) & !String.IsNullOrEmpty(DenyRoles))
            {
                filterContext.Result = OnlyDenyPermission(filterContext.Result);
                filterContext.Result = OnlyAllowPermission(filterContext.Result);
            }
        }

        #endregion

        /// <summary>
        /// Если заданы толь запрещающие привелегии
        /// </summary>
        /// <param name="default">Результат действия в контроллере по-умолчанию</param>
        /// <returns>Результат действия в контроллере</returns>
        private ActionResult OnlyDenyPermission(ActionResult @default)
        {
            string[] roles = DenyRoles.Split(',');

            foreach (string role in roles)
            {
                if (role == UserRole || role == DefaultRoles.All)
                {
                    return RedirectResult;
                }
            }

            return @default;
        }

        /// <summary>
        /// Если заданы толь разрешающие привелегии
        /// </summary>
        /// <param name="default">Результат действия в контроллере по-умолчанию</param>
        /// <returns>Результат действия в контроллере</returns>
        private ActionResult OnlyAllowPermission(ActionResult @default)
        {
            string[] roles = AllowRoles.Split(',');

            bool isQuick = false;
            foreach (string role in roles)
            {
                if (role == DefaultRoles.QuickLogin) isQuick = true;
                if (role == UserRole) return @default;
            }
            return  isQuick ? RedirectResultForQuickLogin : RedirectResult;
        }
    }
}