using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptocurrencyController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptoApiCallerService _cacs;
        private readonly ICryptoApiCallerService _cryptoApiCallerService;

        public CryptocurrencyController(IUnitOfWork unitOfWork, ICryptoApiCallerService cacs, ICryptoApiCallerService cryptoApiCallerService)
        {
            _unitOfWork = unitOfWork;
            _cacs = cacs;
            _cryptoApiCallerService = cryptoApiCallerService;
        }

        // GET: api/<CryptocurrencyController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cryptocurrency>>> Get()
        {
            var result = await _unitOfWork.CryptoCurrencyRepository.GetAll();
            return Ok(result);
        }

        // GET api/<CryptocurrencyController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            var result = await _unitOfWork.CryptoCurrencyRepository.Get(id);
            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET api/<CryptocurrencyController>/5
        [HttpGet("price/{coingeckoId}")]
        public async Task<ActionResult<string>> GetPrice(string coingeckoId)
        {
            var result = await _cryptoApiCallerService.GetCryptoPrice(coingeckoId);
            return Ok(result);
        }

        // POST api/<CryptocurrencyController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CryptocurrencyController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CryptocurrencyController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
