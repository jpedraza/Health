using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Health.Site.Models.Configuration;

namespace Health.Site.Models.Providers
{
    public class MetadataProviderBindingContext
    {
        public Type ModelType { get; set; }

        public Type ProviderType { get; set; }

        public Type ConfigurationType { get; set; }
    }


    /// <summary>
    /// Биндинг типа модели на тип провайдера метаданных.
    /// </summary>
    public class ModelMetadataProviderBinder
    {
        protected List<MetadataProviderBindingContext> Binding { get; set; }

        protected Type CurrentModelType { get; set; }

        public ModelMetadataProviderBinder()
        {
            Binding = new List<MetadataProviderBindingContext>();
        }

        public ModelMetadataProviderBinder Bind(Type model_type)
        {
            CurrentModelType = model_type;
            return this;
        }

        public ModelMetadataProviderBinder Bind<TModel>()
        {
            return Bind(typeof(TModel));
        }

        public void To(Type provider_type, Type configuration_type)
        {
            Binding.Add(new MetadataProviderBindingContext
                            {
                                ModelType = CurrentModelType,
                                ProviderType = provider_type,
                                ConfigurationType = configuration_type
                            });
        }

        public void To<TProvider, TConfiguration>()
            where TProvider : AssociatedMetadataProvider
            where TConfiguration : IMetadataConfigurationProvider
        {
            To(typeof(TProvider), typeof(TConfiguration));
        }

        public AssociatedMetadataProvider ResolveProvider(Type model_type)
        {
            foreach (var type in Binding)
            {
                if (type.ModelType == model_type)
                {
                    var configuration_provider = (IMetadataConfigurationProvider)Activator.CreateInstance(type.ConfigurationType);
                    return Activator.CreateInstance(type.ProviderType, configuration_provider, this) as AssociatedMetadataProvider;
                }
            }
            return null;
        }

        public AssociatedMetadataProvider ResolveProvider<TModel>()
        {
            return ResolveProvider(typeof (TModel));
        }

        public IMetadataConfigurationProvider ResolveConfiguration(Type model_type)
        {
            foreach (var type in Binding)
            {
                if (type.ModelType == model_type)
                {
                    return (IMetadataConfigurationProvider)Activator.CreateInstance(type.ConfigurationType);
                }
            }
            return null;
        }

        public bool IsHaveConfiguration(Type model_type)
        {
            foreach (var type in Binding)
            {
                if (type.ModelType == model_type && type.ConfigurationType != null)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsHaveMetadataProvider(Type model_type)
        {
            foreach (var type in Binding)
            {
                if (type.ModelType == model_type && type.ProviderType != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}