using System.Web.Mvc;

namespace Health.Site.Attributes
{
    /// <summary>
    /// Атрибут контроля доступа к контроллерам
    /// </summary>
    public class Auth : ActionFilterAttribute
    {
        /// <summary>
        /// Разрешенные роли (Приоритет выше, чем у DenyRoles)
        /// </summary>
        public string AllowRoles;

        /// <summary>
        /// Запрещенные роли
        /// </summary>
        public string DenyRoles;

        public override void OnActionExecuting(ActionExecutingContext filter_context)
        {
        }
    }
}