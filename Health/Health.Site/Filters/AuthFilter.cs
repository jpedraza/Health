using System;
using System.Web.Mvc;
using System.Web.Routing;
using Health.API;

namespace Health.Site.Filters
{
    public class AuthFilter : IAuthorizationFilter
    {
        /// <summary>
        /// Куда переадресуем если запрещен доступ
        /// </summary>
        protected readonly RedirectToRouteResult RedirectResult = new RedirectToRouteResult(
            new RouteValueDictionary(new { area = "Account", controller = "Authorization", action = "Login" }));

        /// <summary>
        /// Разрешенные роли (Приоритет выше, чем у DenyRoles)
        /// </summary>
        public string AllowRoles;

        /// <summary>
        /// Запрещенные роли
        /// </summary>
        public string DenyRoles;

        private string _userRole;

        public AuthFilter(ICoreKernel core_service, string allow_roles, string deny_roles)
        {
            CoreServ = core_service;
            AllowRoles = allow_roles;
            DenyRoles = deny_roles;
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
                    _userRole = CoreServ.AuthServ.UserCredential.Role;
                }
                return _userRole;
            }
        }

        /// <summary>
        /// Центральный сервис
        /// </summary>
        protected ICoreKernel CoreServ { get; set; }

        #region IAuthorizationFilter Members

        public void OnAuthorization(AuthorizationContext filter_context)
        {
            // Если у пользователя вообще нет роли (никакой ?)
            if (String.IsNullOrEmpty(CoreServ.AuthServ.UserCredential.Role))
            {
                // Сбрасываем его сессию до гостя
                CoreServ.AuthServ.Logout();
            }

            // Если не заданы права доступа
            if (String.IsNullOrEmpty(AllowRoles) & String.IsNullOrEmpty(DenyRoles))
            {
                // Считаем что всем авторизованным пользователям разрешен доступ
                if (!CoreServ.AuthServ.UserCredential.IsAuthirization)
                {
                    filter_context.Result = RedirectResult;
                    return;
                }
            }

            // Если указаны только права на запрет доступа
            if (String.IsNullOrEmpty(AllowRoles) & !String.IsNullOrEmpty(DenyRoles))
            {
                filter_context.Result = OnlyDenyPermission(filter_context.Result);
                return;
            }

            // Если указаны права только на разрешение доступа
            if (!String.IsNullOrEmpty(AllowRoles) & String.IsNullOrEmpty(DenyRoles))
            {
                filter_context.Result = OnlyAllowPermission(filter_context.Result);
                return;
            }


            if (!String.IsNullOrEmpty(AllowRoles) & !String.IsNullOrEmpty(DenyRoles))
            {
                filter_context.Result = OnlyDenyPermission(filter_context.Result);
                filter_context.Result = OnlyAllowPermission(filter_context.Result);
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
                if (role == UserRole || role == CoreServ.AuthServ.DefaultRoles.All.Name)
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

            foreach (string role in roles)
            {
                if (role == UserRole)
                {
                    return @default;
                }
            }
            return RedirectResult;
        }
    }
}