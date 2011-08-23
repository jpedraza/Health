namespace Health.Core.Entities.POCO
{
    public class Parameter : Entity
    {
        public string Name { get; set; }

        public object Value { get; set; }

        //public MetaData[] MetaData { get; set; }
    }
}