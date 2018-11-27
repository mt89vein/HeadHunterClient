using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;

namespace Domain.Services
{
    public interface IProfessionalAreaService : ISynchronizableService<ProfessionalArea>
    {
        void SaveSpecializations(IReadOnlyCollection<Specialization> specializations);
    }
}