using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstractions;

namespace Domain.Services
{
    public interface ISynchronizableService<in TExternalEntity> : IBaseService<TExternalEntity>
    where TExternalEntity : ExternalEntity
    {
        Task Synchronize(CancellationToken cancellationToken);

        TimeSpan Periodicity { get; }
    }
}