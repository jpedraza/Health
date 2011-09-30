using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Health.Site.Repository
{
    public class ValidationMetadataBinding
    {
        public AssociatedMetadataTypeTypeDescriptionProvider Provider { get; set; }
        public Type For { get; set; }
        public Type Use { get; set; }
    }

    public class ValidationMetadataRepository
    {
        private readonly IList<ValidationMetadataBinding> _validation;

        public ValidationMetadataRepository()
        {
            _validation = new List<ValidationMetadataBinding>();
        }

        public void Add(Type @for, Type use)
        {
            var provider = new AssociatedMetadataTypeTypeDescriptionProvider(@for, use);
            _validation.Add(new ValidationMetadataBinding{For = @for, Use = use, Provider = provider});
        }

        public AssociatedMetadataTypeTypeDescriptionProvider GetForType(Type @for)
        {
            foreach (ValidationMetadataBinding binding in _validation)
            {
                if (binding.For == @for)
                {
                    var provider = binding.Provider;
                    return provider;
                }
            }
            return null;
        }

        public IList<ValidationMetadataBinding> GetAll()
        {
            return _validation;
        }
    }
}