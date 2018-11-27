using System;

namespace HeadHunter.Integration.Abstractions
{
    /// <summary>
    /// Базовый класс для моделей Json представленными в виде Enum 
    /// </summary>
    /// <typeparam name="T">Enum</typeparam>
    public abstract class HeadHunterEnumJsonModel<T> : IHeadHunterJsonModel<T>
    {
        /// <summary>
        /// Получить значение поля, которое хранит значение перечисляемого свойства
        /// </summary>
        /// <returns>Перечисляемое значение</returns>
        protected abstract string GetEnumProperty(); 

        /// <summary>
        /// Получить Enum из модели Json
        /// </summary>
        /// <returns></returns>
        public T GetModel()
        {
            return (T)Enum.Parse(typeof(T), GetEnumProperty(), true);
        }
    }
}