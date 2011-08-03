using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Health.API;
using Health.Site.Models;
using Microsoft.Practices.ServiceLocation;
using Ninject;

namespace Health.Site.Helpers
{
    /// <summary>
    /// Хелпер для вывода главного меню сайта
    /// </summary>
    public static class MainMenuHelper
    {
        /// <summary>
        /// Элементы меню
        /// </summary>
        private static IList<MenuElement> _elements;

        /// <summary>
        /// Центральный сервис
        /// </summary>
        [Inject]
        public static ICoreKernel CoreServ { get; set; }

        /// <summary>
        /// Получить элементы меню
        /// </summary>
        private static void GetMainMenuElements()
        {
            // По-умолчанию ссылка на гланую страницу
            var elements = new List<MenuElement> {new MenuElement("Главная", "Index", "Home")};
            string role = CoreServ.AuthServ.UserCredential.Role;
            switch (role)
            {
                case "Guest":
                    {
                        elements.Add(new MenuElement("Вход", "Login", "Authorization", "Account"));
                        elements.Add(new MenuElement("Регистрация", "Registration", "Registration", "Account"));
                        break;
                    }
                case "Admin":
                    {
                        elements.Add(new MenuElement("Личный кабинет", "Index", "Home", "Admin"));
                        break;
                    }
            }
            if (role != "Guest")
            {
                elements.Add(new MenuElement("Выход", "Logout", "Authorization", "Account"));
            }

            _elements = elements;
        }

        /// <summary>
        /// Преобразуем список элементов в html код
        /// </summary>
        /// <param name="helper"></param>
        /// <returns></returns>
        private static MvcHtmlString GetHtmlString(HtmlHelper helper)
        {
            string menu = string.Format("<li>Здраствуй, {0}</li>", CoreServ.AuthServ.UserCredential.Login);
            foreach (MenuElement element in _elements)
            {
                menu += "<li>" + helper.ActionLink(element.Title, element.Action, element.Controller, new { area = element.Area }, null) + "</li>";
            }

            return MvcHtmlString.Create(menu);
        }

        /// <summary>
        /// Точка входа в хелпер
        /// </summary>
        /// <param name="helper">Центральный класс хелперов</param>
        /// <param name="core_kernel"></param>
        /// <returns>Html код для меню</returns>
        public static MvcHtmlString MainMenu(this HtmlHelper helper, ICoreKernel core_kernel)
        {
            //TODO: Убрать этот костыль
            //CoreServ = ServiceLocator.Current.GetInstance<ICoreKernel>();
            CoreServ = core_kernel;
            GetMainMenuElements();
            return GetHtmlString(helper);
        }
    }
}