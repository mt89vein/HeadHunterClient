namespace HeadHunter.Integration.Abstractions
{
    /// <summary>
    /// Настройки синхронизатора
    /// </summary>
    public abstract class SynchronizerSettings
    {
        /// <summary>
        /// Ссылка, откуда синхронизировать
        /// </summary>
        public string EndpointUrl { get; set; }

        /// <summary>
        /// Периодичность синхронизации в минутах
        /// </summary>
        public int Periodicity { get; set; }

        /// <summary>
        /// Свойство, в котором содержатся необходимые данные
        /// </summary>
        public string PropertyName { get; set; }
    }
}