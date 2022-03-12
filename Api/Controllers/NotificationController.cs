using Api.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBalanceService _balanceService;
        private readonly IClaimsRetriever _claimsRetriever;

        public NotificationController(IUnitOfWork unitOfWork, IBalanceService balanceService, IClaimsRetriever claimsRetriever)
        {
            _unitOfWork = unitOfWork;
            _balanceService = balanceService;
            _claimsRetriever = claimsRetriever;
        }

        [HttpGet]
        public async Task<ActionResult<Notification>> GetDistinctCryptocurrenciesInNotifications()
        {
            var result = await _unitOfWork.NotificationRepository.GetDistinctCryptocurrencyNames();
            return Ok(result);
        }
    }
}
