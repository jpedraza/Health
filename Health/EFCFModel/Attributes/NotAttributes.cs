using System;

namespace EFCFModel.Attributes
{
    /// <summary>
    /// Означает что свойство не будет маппится.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotMapAttribute : Attribute, IMapAttribute
    {
    }

    /// <summary>
    /// Означает что свойство не будет отображаться пользователю при просмотре/редактировании/добавлении.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotDisplayAttribute : Attribute, IDisplayAttribute
    {
    }

    /// <summary>
    /// Означает что свойство будет скрыто от пользователя при просмотре/редактировании/добавлении.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class HideAttribute : Attribute, IDisplayAttribute
    {
    }

    /// <summary>
    /// Означает что свойство не будет доступно для изменения при редактировании/добавлении.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NotEditAttribute : Attribute, IDisplayAttribute
    {
    }
}