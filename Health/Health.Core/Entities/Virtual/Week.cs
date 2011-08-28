namespace Health.Core.Entities.Virtual
{
    public enum ParityOfWeek
    {
        Odd = 1,
        Even = 2,
        All = 0
    }

    public class Week
    {
        public ParityOfWeek Parity { get; set; }

        public string Name { get; set; }

        public int InMonth { get; set; }
    }
}