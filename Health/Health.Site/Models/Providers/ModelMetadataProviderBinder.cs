using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Health.Site.Models.Configuration;

namespace Health.Site.Models.Providers
{
    /// <summary>
    /// Модель для биндинга.
    /// </summary>
    public class MetadataProviderBindingModel
    {
        /// <summary>
        /// Тип модели.
        /// </summary>
        public Type ModelType { get; set; }

        /// <summary>
        /// Тип провайдера.
        /// </summary>
        public Type ProviderType { get; set; }

        /// <summary>
        /// Тип конфигурации.
        /// </summary>
        public Type ConfigurationType { get; set; }
    }

    /// <summary>
    /// Кеш-класс для провайдеров метаданных.
    /// </summary>
    public class MetadataProviderCache
    {
        /// <summary>
        /// Тип провайдера.
        /// </summary>
        public Type ProviderType { get; set; }

        /// <summary>
        /// Тип конфигурации.
        /// </summary>
        public Type ConfigurationType { get; set; }

        /// <summary>
        /// Экземпляр провайдера.
        /// </summary>
        public AssociatedMetadataProvider Provider { get; set; }
    }

    /// <summary>
    /// Кэш-класс для провайдеров конфигурации.
    /// </summary>
    public class MetadataConfigurationCache
    {
        public Type ConfigurationType { get; set; }

        public IMetadataConfigurationProvider ConfigurationProvider { get; set; }
    }


    /// <summary>
    /// Биндинг типа модели на тип провайдера метаданных.
    /// </summary>
    public class ModelMetadataProviderBinder
    {
        /// <summary>
        /// Список биндов.
        /// </summary>
        protected List<MetadataProviderBindingModel> Binding { get; set; }

        /// <summary>
        /// Кэш провайдеров метаданных.
        /// </summary>
        protected List<MetadataProviderCache> ProviderCache { get; set; }

        /// <summary>
        /// Кэш провайдеров конфигурации.
        /// </summary>
        protected List<MetadataConfigurationCache> ConfigurationCache { get; set; }

        /// <summary>
        /// Текущий тип модели.
        /// </summary>
        private Type CurrentModelType { get; set; }

        public ModelMetadataProviderBinder()
        {
            Binding = new List<MetadataProviderBindingModel>();
            ProviderCache = new List<MetadataProviderCache>();
            ConfigurationCache = new List<MetadataConfigurationCache>();
        }

        /// <summary>
        /// Начинает процесс биндинга типа модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Текущий провайдер биндинга.</returns>
        public ModelMetadataProviderBinder Bind(Type model_type)
        {
            CurrentModelType = model_type;
            return this;
        }

        /// <summary>
        /// Generic - метод для начала процесса биндинга типа модели.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public ModelMetadataProviderBinder Bind<TModel>()
        {
            return Bind(typeof(TModel));
        }

        /// <summary>
        /// Текущий тип модели биндится на заданный тип повайдера и тип провайдера конфигурации.
        /// </summary>
        /// <param name="provider_type">Тип провайдера.</param>
        /// <param name="configuration_type">Тип провайдера конйигурации.</param>
        public void To(Type provider_type, Type configuration_type)
        {
            Binding.Add(new MetadataProviderBindingModel
                            {
                                ModelType = CurrentModelType,
                                ProviderType = provider_type,
                                ConfigurationType = configuration_type
                            });
        }

        /// <summary>
        /// Текущий тип модели биндится на заданный тип повайдера и тип провайдера конфигурации.
        /// </summary>
        /// <typeparam name="TProvider">Тип провайдера.</typeparam>
        /// <typeparam name="TConfiguration">Тип провайдера конйигурации.</typeparam>
        public void To<TProvider, TConfiguration>()
            where TProvider : AssociatedMetadataProvider
            where TConfiguration : IMetadataConfigurationProvider
        {
            To(typeof(TProvider), typeof(TConfiguration));
        }

        /// <summary>
        /// Разрешить зависимость типа модели от типа провайдера метаданных.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Экземпляр провайдера метаданных.</returns>
        public AssociatedMetadataProvider ResolveProvider(Type model_type)
        {
            foreach (var binding_model in Binding)
            {
                if (binding_model.ModelType == model_type)
                {
                    foreach (MetadataProviderCache provider_cache in ProviderCache)
                    {
                        if (binding_model.ProviderType == provider_cache.ProviderType && binding_model.ConfigurationType == provider_cache.ConfigurationType)
                        {
                            return provider_cache.Provider;
                        }
                    }
                    IMetadataConfigurationProvider configuration_provider;
                    AssociatedMetadataProvider provider;
                    foreach (MetadataConfigurationCache configuration_cache in ConfigurationCache)
                    {
                        if (configuration_cache.ConfigurationType == binding_model.ConfigurationType)
                        {
                            configuration_provider = configuration_cache.ConfigurationProvider;
                            provider = Activator.CreateInstance(binding_model.ProviderType, configuration_provider, this) as AssociatedMetadataProvider;
                            ProviderCache.Add(new MetadataProviderCache
                                                  {
                                                      ConfigurationType = binding_model.ConfigurationType,
                                                      ProviderType = binding_model.ProviderType,
                                                      Provider = provider
                                                  });
                            return provider;
                        }
                    }
                    configuration_provider = (IMetadataConfigurationProvider)Activator.CreateInstance(binding_model.ConfigurationType);
                    provider = Activator.CreateInstance(binding_model.ProviderType, configuration_provider, this) as AssociatedMetadataProvider;
                    return provider;
                }
            }
            return null;
        }

        /// <summary>
        /// Разрешить зависимость типа модели от типа провайдера метаданных.
        /// </summary>
        /// <typeparam name="TModel">Тип модели.</typeparam>
        /// <returns>Экземпляр провайдера метаданных.</returns>
        public AssociatedMetadataProvider ResolveProvider<TModel>()
        {
            return ResolveProvider(typeof (TModel));
        }


        /// <summary>
        /// Разрешить зависимость типа модели от типа провайдера конфигурации.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Экземпляр провайдера конйигурации.</returns>
        public IMetadataConfigurationProvider ResolveConfiguration(Type model_type)
        {
            foreach (var binding_model in Binding)
            {
                if (binding_model.ModelType == model_type)
                {
                    foreach (MetadataConfigurationCache configuration_cache in ConfigurationCache)
                    {
                        if (binding_model.ConfigurationType == configuration_cache.ConfigurationType)
                        {
                            return configuration_cache.ConfigurationProvider;
                        }
                    }
                    return (IMetadataConfigurationProvider)Activator.CreateInstance(binding_model.ConfigurationType);
                }
            }
            return null;
        }

        /// <summary>
        /// Имеет ли тип модели провайдера конфигурации?
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Результат.</returns>
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

        /// <summary>
        /// Имеет ли тип модели провайдера метаданных.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Результат.</returns>
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

        /// <summary>
        /// Добавляет новый провайдер конфигурации в кэш.
        /// Использовать в том случае, если провайдер конфигурации не имеет стандартного конструктора.
        /// </summary>
        /// <param name="configuration_provider">Провайдер конфигурации.</param>
        public void AddConfigurationProvider(IMetadataConfigurationProvider configuration_provider)
        {
            ConfigurationCache.Add(new MetadataConfigurationCache
                                       {
                                           ConfigurationType = configuration_provider.GetType(),
                                           ConfigurationProvider = configuration_provider
                                       });
        }
    }
}