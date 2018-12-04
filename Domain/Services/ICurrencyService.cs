using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;

namespace Domain.Services
{
    public interface ICurrencyService : ISynchronizableService<Currency>
    {
        Task<IReadOnlyList<Currency>> GetByQueryAsync(string query);
    }
}