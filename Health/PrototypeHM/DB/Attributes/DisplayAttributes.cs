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
    /// Определяет значение-коллекцию с динамическим иземенением содержащихся в ней элементов
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=true)]
    public class DinamicCollectionModelAttribute : Attribute, IDisplayAttribute {

        /// <summary>
        /// тип данных элементов коллекции
        /// </summary>
        public Type TypeOfCollectionElement;

        public DinamicCollectionModelAttribute() { }

        public DinamicCollectionModelAttribute(Type typeOfCollectionElement)
        {
            this.TypeOfCollectionElement = typeOfCollectionElement;
        }
    }

    /// <summary>
    /// Определяет - является ли данное св-во сущности простым, или составным
    /// (т.е. для которого необходима отдельная форма редактирования)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple=false, Inherited=false)]
    public class SimpleOrCompoundModelAttribute : Attribute, IDisplayAttribute
    {
        private bool _isCompound;

        /// <summary>
        /// true - если простой, false - если составной
        /// </summary>
        public bool IsSimple
        {
            get { return _isCompound; }
            set { _isCompound = value; }
        }
    }
}
