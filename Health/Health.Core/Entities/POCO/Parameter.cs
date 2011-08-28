using Health.Core.Entities.POCO.Abstract;

namespace Health.Core.Entities.POCO
{
    public class Parameter : IEntity
    {
        public int ParameterId { get; set; }

        public string Name { get; set; }

        public object Value { get; set; }

        public object DefaultValue { get; set; }

        //public MetaData[] MetaData { get; set; }
    }
}