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

    /// <summary>
    /// Определяет значение с возможностью выбора нескольких значений
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MultiSelectEditModeAttribute:Attribute,IDisplayAttribute
    {
        /// <summary>
        /// Тип контекста операции
        /// </summary>
        public Type OperationContext;

        /// <summary>
        /// Свойство источник значения при бинбинге
        /// </summary>
        public string SourcePropery;

        //Конструктор:
        public MultiSelectEditModeAttribute(Type operationContext, string sourceProperty)
        {
            OperationContext = operationContext;
            SourcePropery = sourceProperty;
        }
    }

    /// <summary>
    /// Определяет - является ли данное св-во сущности простым, или составным
    /// (т.е. для которого необходима отдельная форма редактирования)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=false)]
    public class SimpleOrCompoundModelAttribute : Attribute, IDisplayAttribute
    {
        /// <summary>
        /// true - если простой, false - если составной
        /// </summary>
        public bool IsSimple { get; set; }
    }
}
