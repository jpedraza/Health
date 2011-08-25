namespace Health.Site.Helpers.Classes
{
    /// <summary>
    /// Элемент меню
    /// </summary>
    public class MenuElement
    {
        /// <summary>
        /// Создать элмент меню.
        /// </summary>
        /// <param name="title">Заголовок элемента.</param>
        /// <param name="action">Действие.</param>
        /// <param name="controller">Контроллер.</param>
        /// <param name="area">Область.</param>
        public MenuElement(string title, string action, string controller, string area = null)
        {
            Title = title;
            Action = action;
            Controller = controller;
            Area = area;
        }

        /// <summary>
        /// Заголовок элемента.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Действие.
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Контроллер.
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// Область.
        /// </summary>
        public string Area { get; set; }
    }
}