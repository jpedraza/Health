namespace Health.Core.Entities.Virtual
{
    /// <summary>
    /// День.
    /// </summary>
    public class Day
    {
        /// <summary>
        /// Имя дня недели.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер дня в неделе.
        /// </summary>
        public int InWeek { get; set; }

        /// <summary>
        /// Номер дня в месяце.
        /// </summary>
        public int InMonth { get; set; }
    }
}