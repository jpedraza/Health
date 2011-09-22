using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Site.Models.Configuration.Providers;
using Health.Site.Models.Metadata;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Менеджер провайдеров метаданных.
    /// </summary>
    public class ModelMetadataProviderManager : DataAnnotationsModelMetadataProvider
    {
        private readonly IDIKernel _diKernel;

        public ModelMetadataProviderBinder Binder { get; set; }

        protected ModelMetadataProvider DefaultProvider { get; set; }

        public ModelMetadataProviderManager(IDIKernel di_kernel)
        {
            _diKernel = di_kernel;
            DefaultProvider = new DataAnnotationsModelMetadataProvider();
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
            Binder = _diKernel.Get<ModelMetadataProviderBinder>();
            if (Binder.IsHaveMetadataProvider(container_type))
            {
                IEnumerable<ModelMetadata> model_metadata =
                    Binder.ResolveProvider(container_type).GetMetadataForProperties(container, container_type);
                return model_metadata;
            }

            return DefaultProvider.GetMetadataForProperties(container, container_type);
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> model_accessor, Type container_type,
                                                             string property_name)
        {
            Binder = _diKernel.Get<ModelMetadataProviderBinder>();
            if (Binder.IsHaveMetadataProvider(container_type))
            {
                ModelMetadata model_metadata =
                    Binder.ResolveProvider(container_type).GetMetadataForProperty(model_accessor, container_type,
                                                                                  property_name);
                return model_metadata;
            }

            return DefaultProvider.GetMetadataForProperty(model_accessor, container_type, property_name);
        }

        public override ModelMetadata GetMetadataForType(Func<object> model_accessor, Type model_type)
        {
            Binder = _diKernel.Get<ModelMetadataProviderBinder>();
            if (Binder.IsHaveMetadataProvider(model_type))
            {
                ModelMetadata model_metadata = Binder.ResolveProvider(model_type).GetMetadataForType(model_accessor,
                                                                                                     model_type);
                return model_metadata;
            }

            return DefaultProvider.GetMetadataForType(model_accessor, model_type);
        }

        #endregion
    }
}