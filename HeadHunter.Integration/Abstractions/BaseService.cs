using Domain.Abstractions;
using Domain.Services;

namespace HeadHunter.Integration.Abstractions
{
    public abstract class BaseService<TEntity> : IBaseService<TEntity>
        where TEntity : Entity
    {
        public abstract void Save(TEntity entity);
    }
}