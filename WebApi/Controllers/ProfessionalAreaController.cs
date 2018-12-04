using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionalAreaController : ControllerBase
    {
        private readonly IProfessionalAreaService _professionalAreaService;

        public ProfessionalAreaController(IProfessionalAreaService professionalAreaService)
        {
            _professionalAreaService = professionalAreaService ?? throw new ArgumentNullException(nameof(professionalAreaService));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IReadOnlyList<Specialization>> GetByQuery([FromQuery] string query)
        {
            return await _professionalAreaService.GetByQueryAsync(query);
        }

    }
}