using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Attributes;
using Health.Site.Models.Configuration;
using Health.Site.Models.Configuration.Providers;

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

        /// <summary>
        /// Параеметры конструктора, используемые при инициализации экземпляра провайдера конфигурации.
        /// </summary>
        public object[] ConfigurationParameters { get; set; }
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

        public MetadataConfigurationProvider ConfigurationProvider { get; set; }
    }


    /// <summary>
    /// Биндинг типа модели на тип провайдера метаданных.
    /// </summary>
    public class ModelMetadataProviderBinder
    {
        /// <summary>
        /// Список биндов.
        /// </summary>
        protected readonly List<MetadataProviderBindingModel> Binding;

        /// <summary>
        /// Кэш провайдеров конфигурации.
        /// </summary>
        protected readonly List<MetadataConfigurationCache> ConfigurationCache;

        /// <summary>
        /// Кэш провайдеров метаданных.
        /// </summary>
        protected readonly List<MetadataProviderCache> ProviderCache;

        private readonly IDIKernel _diKernel;

        public ModelMetadataProviderBinder(IDIKernel di_kernel)
        {
            _diKernel = di_kernel;
            Binding = new List<MetadataProviderBindingModel>();
            ProviderCache = new List<MetadataProviderCache>();
            ConfigurationCache = new List<MetadataConfigurationCache>();
        }

        /// <summary>
        /// Текущий тип модели.
        /// </summary>
        private Type CurrentModelType { get; set; }

        /// <summary>
        /// Начинает процесс биндинга типа модели.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Текущий провайдер биндинга.</returns>
        public ModelMetadataProviderBinder For(Type model_type)
        {
            CurrentModelType = model_type;
            return this;
        }

        /// <summary>
        /// Generic - метод для начала процесса биндинга типа модели.
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public ModelMetadataProviderBinder For<TModel>()
        {
            return For(typeof (TModel));
        }

        /// <summary>
        /// Текущий тип модели биндится на заданный тип повайдера и тип провайдера конфигурации.
        /// </summary>
        /// <param name="provider_type">Тип провайдера.</param>
        /// <param name="configuration_type">Тип провайдера конйигурации.</param>
        public ModelMetadataProviderBinder Use(Type provider_type, Type configuration_type)
        {
            for (int i = 0; i < Binding.Count; i++)
            {
                MetadataProviderBindingModel binding_model = Binding[i];
                if (binding_model.ModelType == CurrentModelType)
                {
                    Binding[i] = new MetadataProviderBindingModel
                                     {
                                         ModelType = CurrentModelType,
                                         ProviderType = provider_type,
                                         ConfigurationType = configuration_type
                                     };
                    return this;
                }
            }
            Binding.Add(new MetadataProviderBindingModel
                            {
                                ModelType = CurrentModelType,
                                ProviderType = provider_type,
                                ConfigurationType = configuration_type
                            });
            return this;
        }

        /// <summary>
        /// Текущий тип модели биндится на заданный тип повайдера и тип провайдера конфигурации.
        /// </summary>
        /// <typeparam name="TProvider">Тип провайдера.</typeparam>
        /// <typeparam name="TConfiguration">Тип провайдера конйигурации.</typeparam>
        public ModelMetadataProviderBinder Use<TProvider, TConfiguration>()
            where TProvider : AssociatedMetadataProvider
            where TConfiguration : MetadataConfigurationProvider
        {
            return Use(typeof (TProvider), typeof (TConfiguration));
        }

        /// <summary>
        /// Разрешить зависимость типа модели от типа провайдера метаданных.
        /// </summary>
        /// <param name="model_type">Тип модели.</param>
        /// <returns>Экземпляр провайдера метаданных.</returns>
        public AssociatedMetadataProvider ResolveProvider(Type model_type)
        {
            foreach (MetadataProviderBindingModel binding_model in Binding)
            {
                if (binding_model.ModelType == model_type)
                {
                    int length = binding_model.ConfigurationParameters == null
                                     ? 1
                                     : binding_model.ConfigurationParameters.Length + 1;
                    var parameters = new object[length];
                    parameters[0] = _diKernel.Get<IDIKernel>();
                    for (int i = 0; i < length - 1; i++)
                    {
                        object parameter = binding_model.ConfigurationParameters[i];
                        parameters[i + 1] = parameter;
                    }
                    var configuration_provider =
                        _diKernel.Get(binding_model.ConfigurationType,
                                      parameters) as MetadataConfigurationProvider;
                    var provider = _diKernel.Get(binding_model.ProviderType, _diKernel, configuration_provider) as
                                   AssociatedMetadataProvider;
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
        public MetadataConfigurationProvider ResolveConfiguration(Type model_type)
        {
            foreach (MetadataProviderBindingModel binding_model in Binding)
            {
                if (binding_model.ModelType == model_type)
                {
                    return (MetadataConfigurationProvider) _diKernel.Get(binding_model.ConfigurationType,
                                                                          binding_model.ConfigurationParameters);
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
            foreach (MetadataProviderBindingModel type in Binding)
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
            foreach (MetadataProviderBindingModel type in Binding)
            {
                if (type.ModelType == model_type && type.ProviderType != null)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Задать параметры конструктора провайдера конфигурации.
        /// </summary>
        /// <param name="configuration_parameters">Массив параметров.</param>
        public void WithConfigurationParameters(params object[] configuration_parameters)
        {
            foreach (MetadataProviderBindingModel binding_model in Binding)
            {
                if (binding_model.ModelType == CurrentModelType)
                {
                    binding_model.ConfigurationParameters = configuration_parameters;
                }
            }
        }

        public object[] GetConfigurationParametersByModelType(Type model_type)
        {
            foreach (MetadataProviderBindingModel binding_model in Binding)
            {
                if (binding_model.ModelType == model_type)
                {
                    return binding_model.ConfigurationParameters;
                }
            }
            return null;
        }
    }
}