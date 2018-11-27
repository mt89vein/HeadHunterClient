using Domain.Abstractions;

namespace Domain.Services
{
    /// <summary>
    /// Базовый интерфейс сервисов
    /// </summary>
    /// <typeparam name="TEntity">Доменная сущность</typeparam>
    public interface IBaseService<in TEntity> 
        where TEntity : Entity
    {
        /// <summary>
        /// Сохранить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        void Save(TEntity entity);
    }
}