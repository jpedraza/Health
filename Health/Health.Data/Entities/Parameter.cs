using Health.API.Entities;

namespace Health.Data.Entities
{
    public class Parameter : IParameter
    {
        #region IParameter Members

        public string Name { get; set; }

        public object Value { get; set; }

        public IMetaData[] MetaData { get; set; }

        #endregion
    }
}