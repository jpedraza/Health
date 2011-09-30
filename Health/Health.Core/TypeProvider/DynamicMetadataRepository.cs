using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Health.Core.TypeProvider
{
    public class DynamicMetadataContext
    {
        public Type For { get; set; }
        public Type Use { get; set; }
    }

    public class DynamicMetadataRepository
    {
        private readonly IList<DynamicMetadataContext> _dynamicMetadataContexts = new List<DynamicMetadataContext>();

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
