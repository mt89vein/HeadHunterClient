using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;

namespace Domain.Services
{
    /// <summary>
    /// Сервис по работе с профессиональными областями и их специализациями
    /// </summary>
    public interface IProfessionalAreaService : ISynchronizableService<ProfessionalArea>
    {
        /// <summary>
        /// Сохранить список специализаций
        /// </summary>
        /// <param name="specializations">Специалзиации для сохранения</param>
        void SaveSpecializations(IReadOnlyCollection<Specialization> specializations);

        /// <summary>
        /// Получить специализации по запросу
        /// </summary>
        /// <param name="query">Запрос</param>
        /// <returns>Список специализаций, удовлетворяющих запросу</returns>
        Task<IReadOnlyList<Specialization>> GetByQueryAsync(string query);
    }
}