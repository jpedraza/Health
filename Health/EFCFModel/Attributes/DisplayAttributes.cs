using System;

namespace Model.Attributes
{
    /// <summary>
    /// ¬озможные форматы свойств при редактировании.
    /// </summary>
    [Flags]
    public enum EditMode
    {
        Multiline = 1
    }

    /// <summary>
    /// јттрибут определ€ет формат свойства при редактировании.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EditModeAttribute : Attribute, IDisplayAttribute
    {
        /// <summary>
        /// ‘ормат свойства при редактировании.
        /// </summary>
        private readonly EditMode _mode;

        public EditModeAttribute(EditMode mode)
        {
            _mode = mode;
        }

        public EditMode GetEditMode()
        {
            return _mode;
        }
    }
}