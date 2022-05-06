using Api.Dtos;
using Api.Extensions;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BalanceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext _context;
        private readonly ICryptoWalletCallerService _cryptoWalletCallerService;
        private readonly ICryptoCurrencyRepository _cryptoCurrencyRepository;

        public BalanceController(IUnitOfWork unitOfWork, UserManager<AppUser> userManager, AppDbContext context
            ,ICryptoWalletCallerService cryptoWalletCallerService, ICryptoCurrencyRepository cryptoCurrencyRepository)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _context = context;
            _cryptoWalletCallerService = cryptoWalletCallerService;
            _cryptoCurrencyRepository = cryptoCurrencyRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var balances = await _unitOfWork.BalanceRepository.GetAllForUser(HttpContext.GetUserId());
            if(balances == null)
                return NotFound();
            return Ok(balances);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<Balance>> Get(int id)
        {
            var balance = await _unitOfWork.BalanceRepository.GetForUser(HttpContext.GetUserId(), id);
            if(balance == null)
                return NotFound();
            return Ok(balance);
        }

        [Authorize]
        [HttpGet("address/{address}/{cryptoId}")]
        public async Task<ActionResult<decimal>> GetWithAddress(string address, int cryptoId)
        {
            var crypto = await _unitOfWork.CryptoCurrencyRepository.Get(cryptoId);
            var balance = await _cryptoWalletCallerService.GetWalletBalance(address, crypto);
            if(balance == null)
                return NotFound();
            return Ok(balance);
        }
    }
}
