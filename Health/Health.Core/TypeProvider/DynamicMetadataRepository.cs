using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Health.Core.TypeProvider
{
    /// <summary>
    /// Контекст привязки типа модели к метаданным.
    /// </summary>
    public class DynamicMetadataContext
    {
        /// <summary>
        /// Тип модели.
        /// </summary>
        public Type For { get; set; }

        /// <summary>
        /// Тип метаданных.
        /// </summary>
        public Type Use { get; set; }
    }

    /// <summary>
    /// Репозиторий динамических метаданных модели.
    /// </summary>
    public class DynamicMetadataRepository
    {
        /// <summary>
        /// Контекст привязки метаданных.
        /// </summary>
        private readonly IList<DynamicMetadataContext> _dynamicMetadataContexts = new List<DynamicMetadataContext>();

        /// <summary>
        /// Привязка метаданных.
        /// </summary>
        /// <param name="for">Тип модели.</param>
        /// <param name="use">Тип метаданных.</param>
        public void Bind(Type @for, Type use)
        {
            if (@for == null || use == null)
            {
                throw new Exception("Один из аргументов не определен.");
            }
            for (int i = 0; i < _dynamicMetadataContexts.Count; i++)
            {
                DynamicMetadataContext context = _dynamicMetadataContexts[i];
                if (context.For == @for)
                {
                    _dynamicMetadataContexts[i].Use = use;
                    return;
                }
            }
            _dynamicMetadataContexts.Add(new DynamicMetadataContext{For = @for, Use = use});
        }

        /// <summary>
        /// Получить тип метаданных для типа модели.
        /// </summary>
        /// <param name="modelType">Тип модели.</param>
        /// <returns>Тип метаданных.</returns>
        public Type GetMetadataType(Type modelType)
        {
            foreach (DynamicMetadataContext context in _dynamicMetadataContexts)
            {
                if (context.For == modelType) return context.Use;
            }
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ModelMetadataProviderBinderAttribute : Attribute
    {
        public Type MetadataType;

        protected ModelMetadataProviderBinderAttribute(Type metadataType)
        {
            MetadataType = metadataType;
        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class ClassMetadataAttribute : ModelMetadataProviderBinderAttribute
    {
        public ClassMetadataAttribute(Type metadataType) : base(metadataType) { }
    }
}
