using System;
using System.ComponentModel;
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
                new DynamicTypeDescriptor(base.GetTypeDescriptor(objectType, instance), metadataType);
        }
    }
}
