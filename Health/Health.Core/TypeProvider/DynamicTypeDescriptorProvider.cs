using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Health.Core.API;

namespace Health.Core.TypeProvider
{
    public class DynamicTypeDescriptorProvider : TypeDescriptionProvider
    {
        private readonly IDIKernel _diKernel;

        public DynamicTypeDescriptorProvider(IDIKernel diKernel, Type modelType) : base(TypeDescriptor.GetProvider(modelType)) 
        {
            _diKernel = diKernel;
        }

        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            var repository = _diKernel.Get<DynamicMetadataRepository>();
            Type metadataType = repository.GetMetadataType(objectType);
            if (metadataType == null) return base.GetTypeDescriptor(objectType, instance);
            return
                new DynamicTypeDescriptor(_diKernel, base.GetTypeDescriptor(objectType, instance), objectType, metadataType);
        }
    }
}
