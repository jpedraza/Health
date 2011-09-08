namespace Health.Core.Entities.Virtual
{
    public enum ParityOfWeek
    {
        // Нечетная
        Odd = 1,
        // Четная
        Even = 2,
        // Любая
        All = 0
    }

    public class Week
    {
        public ParityOfWeek Parity { get; set; }

        public string Name { get; set; }

        public int InMonth { get; set; }
    }
}