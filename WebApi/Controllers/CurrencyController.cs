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
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            _currencyService = currencyService ?? throw new ArgumentNullException(nameof(currencyService));
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IReadOnlyList<Currency>> GetByQuery([FromQuery] string query)
        {
            return await _currencyService.GetByQueryAsync(query);
        }
    }
}