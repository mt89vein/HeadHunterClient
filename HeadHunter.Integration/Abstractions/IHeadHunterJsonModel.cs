namespace HeadHunter.Integration.Abstractions
{
    /// <summary>
    /// Интерфейс для JSON моделей HeadHunter
    /// </summary>
    /// <typeparam name="T">Доменная сущность</typeparam>
    public interface IHeadHunterJsonModel<out T>
    {
        /// <summary>
        /// Получить доменную сущность из модели HeadHunter
        /// </summary>
        /// <returns>Доменная сущность</returns>
        T GetModel();
    }
}