using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Менеджер провайдеров метаданных.
    /// </summary>
    public class ModelMetadataProviderManager : DataAnnotationsModelMetadataProvider
    {
        public ModelMetadataProviderBinder Binder { get; set; }

        protected ModelMetadataProvider DefaultProvider { get; set; }

        public ModelMetadataProviderManager(ModelMetadataProviderBinder binder)
        {
            Binder = binder;
            DefaultProvider = new EmptyModelMetadataProvider();
        }

        #region Overrides of ModelMetadataProvider

        /// <summary>
        /// Получает объект <see cref="T:System.Web.Mvc.ModelMetadata"/> для каждого свойства модели.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Web.Mvc.ModelMetadata"/> для каждого свойства модели.
        /// </returns>
        /// <param name="container">Контейнер.</param><param name="container_type">Тип контейнера.</param>
        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type container_type)
        {
            if (Binder.IsHaveMetadataProvider(container_type))
            {
                return Binder.ResolveProvider(container_type).GetMetadataForProperties(container, container_type);
            }

            return DefaultProvider.GetMetadataForProperties(container, container_type);
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> model_accessor, Type container_type, string property_name)
        {
            if (Binder.IsHaveMetadataProvider(container_type))
            {
                return Binder.ResolveProvider(container_type).GetMetadataForProperty(model_accessor, container_type,
                                                                             property_name);
            }

            return DefaultProvider.GetMetadataForProperty(model_accessor, container_type, property_name);
        }

        public override ModelMetadata GetMetadataForType(Func<object> model_accessor, Type model_type)
        {
            if (Binder.IsHaveMetadataProvider(model_type))
            {
                return Binder.ResolveProvider(model_type).GetMetadataForType(model_accessor, model_type);
            }

            return DefaultProvider.GetMetadataForType(model_accessor, model_type);
        }

        #endregion
    }
}