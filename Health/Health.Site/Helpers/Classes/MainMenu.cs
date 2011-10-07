using System.Collections.Generic;
using Health.Core.API;
using Health.Core.API.Services;
using Health.Site.Models;

namespace Health.Site.Helpers.Classes
{
    /// <summary>
    /// Хелпер для вывода главного меню сайта
    /// </summary>
    public class MainMenu
    {
        /// <summary>
        /// Точка входа в хелпер
        /// </summary>
        /// <returns>Html код для меню</returns>
        public MainMenu(IDIKernel di_kernel)
        {
            DIKernel = di_kernel;
        }

        protected IDIKernel DIKernel { get; set; }

        /// <summary>
        /// Получить элементы меню
        /// </summary>
        public List<MenuElement> GetMainMenuElements()
        {
            // По-умолчанию ссылка на гланую страницу
            var elements = new List<MenuElement> {new MenuElement("Главная", "Index", "Home")};
            string role = DIKernel.Get<IAuthorizationService>().UserCredential.Role;
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
                        //elements.Add(new MenuElement("Параметры (старые)", "Index", "Home", "Parameters"));
                        elements.Add(new MenuElement("Расписание", "Index", "Home", "Schedules"));
                        break;
                    }
            }
            if (role != "Guest")
            {
                elements.Add(new MenuElement("Выход", "Logout", "Authorization", "Account"));
            }
            return elements;
        }
    }
}