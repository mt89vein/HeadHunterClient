namespace Domain.Enums
{
    /// <summary>
    /// График работы
    /// </summary>
    public enum ScheduleType
    {
        /// <summary>
        /// Полный день
        /// </summary>
        FullDay,

        /// <summary>
        /// Сменный график
        /// </summary>
        Shift,

        /// <summary>
        /// Гибкий график
        /// </summary>
        Flexible,

        /// <summary>
        /// Удаленная работа
        /// </summary>
        Remote,

        /// <summary>
        /// Вахтовый метод
        /// </summary>
        FlyInFlyOut,
    }
}