namespace Health.API.Entities
{
    /// <summary>
    /// Мандат пользователя.
    /// </summary>
    public interface IUserCredential : IEntity
    {
        /// <summary>
        /// Логин пользователя.
        /// </summary>
        string Login { get; set; }

        /// <summary>
        /// Роль пользователя.
        /// </summary>
        string Role { get; set; }

        /// <summary>
        /// Авторизован ли пользователь.
        /// </summary>
        bool IsAuthirization { get; set; }

        /// <summary>
        /// Запомнен ли пользователь.
        /// </summary>
        bool IsRemember { get; set; }
    }
}