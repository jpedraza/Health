using System.Collections.Generic;
using Health.API;
using Health.Site.Models;

namespace Health.Site.Helpers.Classes
{
    /// <summary>
    /// Хелпер для вывода главного меню сайта
    /// </summary>
    public class MainMenu
    {
        protected ICoreKernel CoreKernel { get; set; }

        /// <summary>
        /// Точка входа в хелпер
        /// </summary>
        /// <param name="core_kernel"></param>
        /// <returns>Html код для меню</returns>
        public MainMenu(ICoreKernel core_kernel)
        {
            CoreKernel = core_kernel;
        }

        /// <summary>
        /// Получить элементы меню
        /// </summary>
        public List<MenuElement> GetMainMenuElements()
        {
            // По-умолчанию ссылка на гланую страницу
            var elements = new List<MenuElement> {new MenuElement("Главная", "Index", "Home")};
            string role = CoreKernel.AuthServ.UserCredential.Role;
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
            return elements;
        }
    }
}