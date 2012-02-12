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
    /// Возможные виды связей между сущностями на основе того как 
    /// они представлены в базе данных.
    /// </summary>
    public enum TypeMappingEnum
    {
        /// <summary>
        /// Данная сущность может быть связана с несколькими
        /// другими сущностями
        /// </summary>
        OneToMany = 1,

        /// <summary>
        /// Многие ко многим
        /// </summary>
        ManyToMany = 2,

        /// <summary>
        /// Сущностей может быть много, но все они принадлежат какой-то конкретной сущности
        /// </summary>
        ManyToOne = 3
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

        /// <summary>
        /// Тип связи между сущностями
        /// </summary>
        public TypeMappingEnum Type { get; set; }

        public SingleSelectEditModeAttribute(Type operationContext, string sourceProperty)
        {
            OperationContext = operationContext;
            SourceProperty = sourceProperty;
        }

        public SingleSelectEditModeAttribute(Type operationContext, string sourceProperty, TypeMappingEnum typeMapping)
        {
            OperationContext = operationContext;
            SourceProperty = sourceProperty;
            Type = typeMapping;
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

        /// <summary>
        /// Тип связи между сущностями
        /// </summary>
        public TypeMappingEnum Type { get; set; }

        //Конструктор:
        public MultiSelectEditModeAttribute(Type operationContext, string sourceProperty)
        {
            OperationContext = operationContext;
            SourcePropery = sourceProperty;
        }

        //Перегрузка конструктора с учетом связи между сущностями
        public MultiSelectEditModeAttribute(Type operationContext, string sourceProperty, TypeMappingEnum typeMapping)
        {
            OperationContext = operationContext;
            SourcePropery = sourceProperty;
            Type = typeMapping;
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
