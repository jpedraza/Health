namespace Health.Site.Models
{
    /// <summary>
    /// Элемент меню
    /// </summary>
    public class MenuElement
    {
        /// <summary>
        /// Создать элмент меню
        /// </summary>
        /// <param name="title">Заголовок элемента</param>
        /// <param name="action">Действие</param>
        /// <param name="controller">Контроллер</param>
        public MenuElement(string title, string action, string controller)
        {
            Title = title;
            Action = action;
            Controller = controller;
        }

        /// <summary>
        /// Заголовок элемента
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Действие
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Контроллер
        /// </summary>
        public string Controller { get; set; }
    }
}