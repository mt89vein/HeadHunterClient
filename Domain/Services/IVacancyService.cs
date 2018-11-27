using System.Threading.Tasks;
using Domain.Core;
using Domain.SearchModels;

namespace Domain.Services
{
    public interface IVacancyService : IBaseService<Vacancy>
    {
        Task<DataPage<Vacancy>> GetFilteredAsync(VacancyFilter filter);
    }
}