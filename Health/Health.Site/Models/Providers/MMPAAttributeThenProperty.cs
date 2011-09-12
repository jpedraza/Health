using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Health.Core.API;
using Health.Site.Models.Configuration;

namespace Health.Site.Models.Providers
{
    public class MMPAAttributeThenProperty : ModelMetadataProviderAdapter
    {
        public MMPAAttributeThenProperty(IDIKernel di_kernel, IMetadataConfigurationProvider configuration_provider)
            : base(di_kernel, configuration_provider)
        {
        }

        #region Overrides of ModelMetadataProviderAdapter

        protected override ModelMetadata InitializeMetadata(ModelMetadata model_metadata,
                                                            ModelMetadataPropertyConfiguration property_configuration,
                                                            IEnumerable<Attribute> attributes, Type container_type,
                                                            Func<object> model_accessor, Type model_type,
                                                            string property_name)
        {
            if (property_configuration.Attributes != null && property_configuration.Attributes.Count > 0)
            {
                model_metadata = InitializeAttributes(model_metadata, property_configuration, container_type,
                                                      model_accessor, model_type,
                                                      property_name);
            }
            model_metadata = InitializeProperties(model_metadata, property_configuration);
            return model_metadata;
        }

        #endregion
    }
}