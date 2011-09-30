using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Web.Mvc;
using Health.Core.API;
using Health.Core.Entities.POCO;
using Health.Core.Entities.Virtual;
using Health.Core.TypeProvider;
using Health.Site.Attributes;
using Health.Site.Models.Configuration.Providers;
using Health.Site.Models.Metadata;
using Health.Site.Repository;

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

        public ModelMetadataProviderManager(IDIKernel diKernel)
        {
            _diKernel = diKernel;
            DefaultProvider = new DataAnnotationsModelMetadataProvider();
        }

        #region Overrides of ModelMetadataProvider

        /// <summary>
        /// Получает объект <see cref="T:System.Web.Mvc.ModelMetadata"/> для каждого свойства модели.
        /// </summary>
        /// <returns>
        /// Объект <see cref="T:System.Web.Mvc.ModelMetadata"/> для каждого свойства модели.
        /// </returns>
        /// <param name="container">Контейнер.</param><param name="containerType">Тип контейнера.</param>
        public override IEnumerable<ModelMetadata> GetMetadataForProperties(object container, Type containerType)
        {
            FindAndBind(containerType);
            IEnumerable<ModelMetadata> modelMetadata = DefaultProvider.GetMetadataForProperties(container, containerType);

            return modelMetadata;
        }

        public override ModelMetadata GetMetadataForProperty(Func<object> modelAccessor, Type containerType,
                                                             string propertyName)
        {
            FindAndBind(containerType);
            ModelMetadata modelMetadata = DefaultProvider.GetMetadataForProperty(modelAccessor, containerType, propertyName);

            return modelMetadata;
        }

        public override ModelMetadata GetMetadataForType(Func<object> modelAccessor, Type modelType)
        {
            FindAndBind(modelType);
            ModelMetadata modelMetadata = DefaultProvider.GetMetadataForType(modelAccessor, modelType);

            return modelMetadata;
        }

        #endregion

        private void FindAndBind(Type modelType)
        {
            if (modelType == null) return;
            PropertyInfo[] properties = modelType.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                object[] attributes = property.GetCustomAttributes(false);
                foreach (object attribute in attributes)
                {
                    if (attribute is ModelMetadataProviderBinderAttribute)
                    {
                        var att = attribute as ModelMetadataProviderBinderAttribute;
                        _diKernel.Get<DynamicMetadataRepository>().Bind(property.PropertyType, att.MetadataType);
                        FindAndBind(att.MetadataType);
                    }
                }
            }
        }
    }
}