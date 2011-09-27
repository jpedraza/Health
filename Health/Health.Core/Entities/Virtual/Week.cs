namespace Health.Core.Entities.Virtual
{
    /// <summary>
    /// Четность недели.
    /// </summary>
    public enum ParityOfWeek
    {
        // Нечетная
        Odd = 1,
        // Четная
        Even = 2,
        // Любая
        All = 0
    }

    /// <summary>
    /// Неделя.
    /// </summary>
    public class Week
    {
        /// <summary>
        /// Четность недели.
        /// </summary>
        public ParityOfWeek Parity { get; set; }

        /// <summary>
        /// Номер недели в месяце.
        /// </summary>
        public int InMonth { get; set; }
    }
}