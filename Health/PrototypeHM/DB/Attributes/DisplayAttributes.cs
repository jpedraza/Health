using System;

namespace PrototypeHM.DB.Attributes
{
    /// <summary>
    /// Возможные форматы свойств при редактировании.
    /// </summary>
    [Flags]
    public enum EditModeEnum
    {
        Multiline = 1
    }

    /// <summary>
    /// Аттрибут определяет формат свойства при редактировании.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EditModeAttribute : Attribute, IDisplayAttribute
    {
        /// <summary>
        /// Формат свойства при редактировании.
        /// </summary>
        public EditModeEnum Mode { get; set; }
    }

    /// <summary>
    /// Определяет значение с возможностью одиночного выбора.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class SingleSelectEditModeAttribute : Attribute, IDisplayAttribute
    {
        /// <summary>
        /// Тип контекста опреации.
        /// </summary>
        public Type OperationContext;

        /// <summary>
        /// Свойство источник значения при бинбинге.
        /// </summary>
        public string SourceProperty;

        public SingleSelectEditModeAttribute(Type operationContext, string sourceProperty)
        {
            OperationContext = operationContext;
            SourceProperty = sourceProperty;
        }
    }
}
