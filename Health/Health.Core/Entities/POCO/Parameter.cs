//TODO: Убрать атрибут Период для Параметра
using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    public class Parameter : IEntity, IKey
    {        
        /// <summary>
        /// Текстовое название параметра.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение параметра (если параметр с вариантами, то сумма баллов).
        /// </summary>
        public object Value {
            get
            {
                if(_Value!=null)
                {
                    var str = _Value as string[];
                    if(str != null)
                    {
                        return str[0];
                    }
                    
                }
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        private object _Value;
        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        public object DefaultValue
        {
            get
            {
                if(_defaultVaue != null)
                {
                    var str = _defaultVaue as string[];
                    if (str != null)
                    {
                        return str[0];
                    }
                    return _defaultVaue;
                }
                return _defaultVaue;
            }
            set { _defaultVaue = value; }
        }

        private object _defaultVaue;
        /// <summary>
        /// Мета-данные (описывают характеристику параметра).
        /// </summary>
        public MetaData MetaData { get; set; }

        #region Implementation of IKey

        /// <summary>
        /// Идентификатор сущности.
        /// </summary>
        public int Id { get; set; }

        #endregion
    }
}